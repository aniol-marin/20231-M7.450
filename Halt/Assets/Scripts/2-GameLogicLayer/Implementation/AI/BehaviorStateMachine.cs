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
        [Injected] private readonly InteractionManager interactions;
        [Injected] private readonly IQueueManager queue;
        [Injected] private readonly ActivityManager activityManager;
        private BehaviorType currentBehavior;
        private EntityId id;
        private List<Trigger> activeTriggers = new();
        private StateMachineWiring wiring;
        private bool actionTriggered = false;

        public ControllerType Type => ControllerType.stateMachine;

        public event Callback<Position> OnMoveToPositionRequest = delegate { };

        public void Initialize(EntityId id, StateMachineWiring wiring)
        {
            this.id = id;
            this.wiring = wiring;

            interactions.OnInteractionReported += HandleInteraction;

            //Kickstart
            currentBehavior = wiring.InitialBehavior;
            Kick();
        }

        public void Deinitialize()
        {
            id = default;
            actionTriggered = false;
        }

        private void HandleInteraction(EntityId id, Entity target)
        {
            if (this.id == id)
            {
                new Error("getting an input");

                // Adapt state before kicking
                Kick();
            }
        }

        private void Kick()
        {
            IEnumerable<Reaction> reactions = wiring
                .States
                .Single(s => s.behavior == currentBehavior)
                .Store(out State state)
                .reactions;

            if (!actionTriggered)
            {
                actionTriggered = true;
                activityManager.StartActivity(state.activity, id);
                OrderEvent order = new OrderEvent(state.activity, id);
                queue.ReportEvent(new OrderEvent(state.activity, id));
            }

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

            new Error($"Kicking entity {id} matched {matches.Count()} active triggers");


            if (wiring.States.Multiple() && reactions.Empty())
            {
                new Warning($"Entity [{id}]'s state machine reached a dead end");
            }
        }

        private void HandleTriggeredReactions(Reaction reaction)
        {
            new Error($"Handling a triggered reaction in entity {id}");

        }

    }
}