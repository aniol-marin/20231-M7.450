using Mole.Halt.DataAccessLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using System.Collections.Generic;

namespace Mole.Halt.GameLogicLayer
{
    public class CensusManager : Initializable, ICensusManager
    {
        [Injected] private readonly IQueueManager queue;
        [Injected] private readonly ICensusPackage census;
        private readonly List<BehaviorManager> behaviorManagers = new();

        public event Callback<EntityId> OnCharacterRegistered = delegate { };

        public IEnumerable<EntityId> Active => census.Active;

        public void Init()
        {
            census.Clear();
            queue.OnEventDequeued += HandleEvent;
        }

        public void Deinit()
        {
            queue.OnEventDequeued -= HandleEvent;
        }

        public void AddBehaviorManager(BehaviorManager manager)
        {
            behaviorManagers.Add(manager);
        }

        public void RemoveBehaviorManager(BehaviorManager manager)
        {
            behaviorManagers.Remove(manager);
        }
        private void HandleEvent(GameEvent parameter)
        {
            if (parameter is RegistrationEvent registration && registration.entity != null && registration.entity.EntityType == EntityType.Character)
            {
                census.AddEntity(registration.entity.Id);
                OnCharacterRegistered(registration.entity.Id);
            }
        }
    }
}
