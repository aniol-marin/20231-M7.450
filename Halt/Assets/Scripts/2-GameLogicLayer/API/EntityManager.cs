using Mole.Halt.DataAccessLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using System.Collections.Generic;

namespace Mole.Halt.GameLogicLayer
{
    public class EntityManager : Initializable
    {
        [Injected] private readonly IQueueManager queue;
        [Injected] private readonly ICharacterRepository characters;
        [Injected] private readonly IObjectRepository objects;
        private readonly List<BehaviorManager> behaviorManagers = new();

        public void Init()
        {
            queue.OnEventDequeued += HandleEvent;
        }

        public void Deinit()
        {
            queue.OnEventDequeued -= HandleEvent;
        }
        private void HandleEvent(GameEvent parameter)
        {
            if (parameter is RegistrationEvent registration)
            {
                switch (registration.entity?.GetType().Name)
                {
                    case nameof(Object): objects.Add(registration.entity as Object); break;
                    case nameof(Path): objects.Add(registration.entity as Path); break;
                    case nameof(Character): characters.Add(registration.entity as Character); break;
                    default: new Error($"trying to register an invalid entity: {registration.entity?.GetType().Name ?? "null"}"); break;
                }
            }
        }
    }
}
