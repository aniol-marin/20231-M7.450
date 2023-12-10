using Mole.Halt.ApplicationLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Mole.Halt.PresentationLayer
{
    public class JusticeController : ControllerNode
    {
        [Injected] readonly EntityMappingService mapping;
        static List<CharacterView> views = new();
        static List<CharacterView> cops = new();
        static List<CharacterView> thieves = new();
        static List<CharacterView> victims = new();
        static List<HideoutView> hideouts = new();
        static Dictionary<int, int> activeChases = new();
        static Dictionary<int, EntityId> activeCooldowns = new();

        private void Start()
        {
            hideouts.AddRange(
                FindObjectsByType<HideoutView>(FindObjectsSortMode.None));

            views.AddRange(
                FindObjectsByType<CharacterView>(FindObjectsSortMode.None));

            cops.AddRange(views
                .Where(v => mapping.GetCharacterType(v) == DataLayer.CharacterType.cop));

            thieves.AddRange(views
                .Where(v => mapping.GetCharacterType(v) == DataLayer.CharacterType.thief));

            victims.AddRange(views
                .Where(v => mapping.GetCharacterType(v) == DataLayer.CharacterType.pensioner));

            Assert.IsTrue(hideouts.Any());
            Assert.IsTrue(views.Any());
            Assert.IsTrue(cops.Any());
            Assert.IsTrue(thieves.Any());
            Assert.IsTrue(victims.Any());
        }

        public void NotifyCopChasing(int copId)
        {
            activeChases[copId] = thieves
                .OrderBy(t => Vector3.Distance(cops.First().transform.position, t.transform.position))
                .First()
                .JusticeId;
        }

        public static bool IsCopChasing(int copId)
        {
            return activeChases.Keys.Any(v => v == copId);
        }

        public static bool IsThiefBeingChased(int thiefId)
        {
            return activeChases.Values.Any(v => v == thiefId);
        }

        public static Position GetOwnPosition(int id)
        {
            return views
                 .First(t => t.JusticeId == id)
                 .GetPosition();
        }

        public static bool IsChasedThiefHidden(int copId)
        {
            bool hidden;

            if (activeChases.TryGetValue(copId, out var thiefId))
            {
                Position thief = thieves
                    .First(t => t.JusticeId == thiefId)
                    .GetPosition();
                hidden = hideouts.Any(h => h.IsPositionInHideout(thief));
            }
            else
            {
                hidden = false;
            }

            return hidden;
        }

        public static Position GetPositionOfNearestHideout(int thiefId)
        {
            Position thiefPosition = thieves
                .First(t => t.JusticeId == thiefId)
                .GetPosition();

            return hideouts
                .OrderBy(v => v.GetPosition().Distance(thiefPosition))
                .Last()
                .GetPosition();
        }

        public static Position GetPositionOfNearestCop(int thiefId)
        {
            Position thiefPosition = thieves
                .First(t => t.JusticeId == thiefId)
                .GetPosition();

            return cops
                .OrderBy(v => v.GetPosition().Distance(thiefPosition))
                .Last()
                .GetPosition();
        }

        public static Position GetPositionOfNearestVictim(int thiefId)
        {
            Position thiefPosition = thieves
                .First(t => t.JusticeId == thiefId)
                .GetPosition();

            return victims
                .OrderBy(v => v.GetPosition().Distance(thiefPosition))
                .Last()
                .GetPosition();
        }

        public static CharacterView GetNearestVictim(int thiefId)
        {
            Position thiefPosition = thieves
                .First(t => t.JusticeId == thiefId)
                .GetPosition();

            return victims
                .OrderBy(v => v.GetPosition().Distance(thiefPosition))
                .Last();
        }

        public static Position GetPositionOfChasedThief(int copId)
        {
            return thieves
                .First(t => t.JusticeId == activeChases[copId])
                .GetPosition();
        }

        public static NavMeshAgent GetAgent(int id)
        {
            return views
                .First(v => v.JusticeId == id)
                .GetComponent<NavMeshAgent>();
        }

        public static void ReportTheftAttemt(int thiefId, bool successful)
        {
            if (!successful)
            {
                Position thiefPosition = thieves
                    .First(t => t.JusticeId == thiefId)
                    .GetPosition();

                int copId = cops
                    .OrderBy(c => c.GetPosition().Distance(thiefPosition))
                    .Last()
                    .JusticeId;

                activeChases[copId] = thiefId;
            }
            else
            {
                Position thiefPosition = thieves.First(t => t.JusticeId == thiefId).GetPosition();
                EntityId unluckyGuy = victims
                    .OrderBy(v => v.GetPosition().Distance(thiefPosition))
                    .Last()
                    .id;

                activeCooldowns[thiefId] = unluckyGuy;
            }
        }

        public static bool IsThiefInCooldown(int thiefId)
        {
            Position thief = thieves
                    .First(t => t.JusticeId == thiefId)
                    .GetPosition();
            bool hidden = hideouts.Any(h => h.IsPositionInHideout(thief));
            
            if (hidden)
            {
                activeCooldowns.Remove(thiefId);
            }

            return activeCooldowns.ContainsKey(thiefId);
        }

        public static void AttemptTheft(int thiefId, CharacterView victim)
        {
            var thief = thieves
                    .First(t => t.JusticeId == thiefId);

            thief
                .GetComponent<CharacterView>()
                .SetChaseTarget(victim.transform);

        }

        public override void Deinit()
        {
        }
    }
}