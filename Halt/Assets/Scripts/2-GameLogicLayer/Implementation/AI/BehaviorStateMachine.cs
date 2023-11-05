using Mole.Halt.DataAccessLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mole.Halt.GameLogicLayer.Internal
{
    public class BehaviorStateMachine : BehaviorManager
    {
        [Injected] private readonly ActivityManager activityManager;
        [Injected] private readonly InteractionManager interactions;
        [Injected] private readonly IFilterMatchingService service;
        [Injected] private readonly ITimeline timeline;
        private StateMachineWiring wiring;
        private State activeState;
        private readonly List<Trigger> activeTriggers = new();
        private EntityId id;
        private bool actionTriggered = false;
        private Activity activeTask;

        public void Initialize(EntityId id, StateMachineWiring wiring)
        {
            this.id = id;
            this.wiring = wiring;

            interactions.OnInteractionReported += HandleInteraction;

            //Kickstart
            timeline.OnPlay += Start;
        }

        public void Deinitialize()
        {
            id = default;
            actionTriggered = false;
            activityManager.StopActivity(activeTask);
        }

        private void Start()
        {
            SelectNewState(wiring.InitialBehavior);
            Kick();
        }

        private void SelectNewState(BehaviorType behavior)
        {
            activityManager.StopActivity(activeTask);

            activeState = wiring
                .States
                .Single(s => s.behavior == behavior);
        }

        private void Kick()
        {
            if (!actionTriggered)
            {
                actionTriggered = true;
                activeTask = activityManager.StartActivity(activeState.activity, id);
            }

            IEnumerable<Reaction> reactions = activeState
                .reactions;

            IEnumerable<Reaction> matches = reactions
                .Where(r => r.triggers.Any(t => activeTriggers.Select(t => t.trigger).Contains(t.trigger)));

            matches
                .FirstOrDefault(triggered)?
                .SideEffect(HandleTriggeredReactions);

            bool triggered(Reaction reaction)
            {
                return false;
                /*
                return activeTriggers
                    .Select(t => t.trigger)
                    .Union(reaction.triggers)
                    .Any();
                 */
            }

            if (wiring.States.Multiple() && reactions.Empty())
            {
                new Warning($"Entity [{id}]'s state machine reached a dead end");
            }
        }

        private void HandleInteraction(EntityId id, Entity target)
        {
            if (this.id == id)
            {
                // hardcoded
                if (target.EntityType == EntityType.Object)
                {
                    if (activeTriggers
                        .Where(t => service.Match(target, t.trigger))
                        .Store(out IEnumerable<Trigger> triggers)
                        .Any())
                    {
                        BehaviorType nextNode = activeState
                               .reactions
                               .First(r => r.triggers.Any(t => triggers.Contains(t)))
                               .consequence;

                        SelectNewState(nextNode);
                    }
                }

                Kick();
            }
        }

        private void HandleTriggeredReactions(Reaction reaction)
        {
            new Error($"Handling a triggered reaction in entity {id}");

        }

    }
}