using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using System.Collections.Generic;

namespace Mole.Halt.GameLogicLayer
{
    public class InteractionManager : Initializable, IInteractionManager
    {
        [Injected] private readonly IQueueManager queue;
        private readonly List<BehaviorManager> behaviorManagers = new();

        public event Callback<EntityId, Entity> OnInteractionReported = delegate { };

        public void Init()
        {
            queue.OnEventDequeued += HandleEvent;
        }

        public void Deinit()
        {
        }

        public void AddBehaviorManager(BehaviorManager manager)
        {
            behaviorManagers.Add(manager);
        }

        public void RemoveBehaviorManager(BehaviorManager manager)
        {
            behaviorManagers.Remove(manager);
        }

        private void HandleEvent(GameEvent gameEvent)
        {
            if (gameEvent is InteractionEvent interaction)
            {
                OnInteractionReported(interaction.effector, interaction.target);
            }
        }
    }
}
