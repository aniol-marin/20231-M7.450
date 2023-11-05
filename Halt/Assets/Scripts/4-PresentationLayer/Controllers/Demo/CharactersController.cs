using Mole.Halt.ApplicationLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Mole.Halt.PresentationLayer
{
    public class CharactersController : ControllerNode
    {
        [Injected(Id = MessageHandlerType.characters)] private readonly MessageHandler messages;
        [Injected] private readonly Allocator instanceProvider;
        [Injected] private readonly ICensusManager census;
        [Injected] private readonly EntityMappingService entityMapping;
        [Injected] private readonly ILevelLoader levelLoader;
        [Injected] private readonly PostOffice postOffice;
        [Injected] private readonly IQueueManager queue;
        private readonly Dictionary<CharacterView, BehaviorStrategy> subcontrollers = new();
        private readonly List<CharacterView> activeCharacters = new();
        private readonly List<CharacterView> pooledCharacters = new();

        public override void Init()
        {
            base.Init();

            levelLoader.OnCharactersRegistered += HandleCharactersRegistered;
            messages.OnMessageReceived += MockHandleCharacterMessage;
        }

        public override void Deinit()
        {

        }

        [Mocked]
        private void MockHandleCharacterMessage()
        {
            activeCharacters
                .ToArray()
                .ForEach(c =>
                {
                    if (census.Active.None(a => a == c.id))
                    {
                        Destroy(c);
                        activeCharacters.Remove(c);
                    }
                });
        }

        private void HandleCharactersRegistered(IEnumerable<GeneratedCharacter> characters)
        {
            AddCharacters(characters);
        }

        public void AddCharacters(IEnumerable<GeneratedCharacter> characters)
        {
            activeCharacters.AddRange(characters.Select(c => c.view));
            characters
                .ForEach(c =>
                {
                    CharacterView.Initializer values = new(
                        onColliderContactLost: HandleColliderExit,
                        onNewColliderDetected: HandleColliderEnter,
                        onSameColliderDetected: HandleColliderStay);

                    c.view.Init(values);

                    BehaviorStrategy strategy = instanceProvider.Instantiate<BehaviorStrategy>();
                    strategy.Initialize(c.character.Id, c.controller, c.view.GiveOrder);
                    subcontrollers.Add(c.view, strategy);
                });
        }

        private void HandleColliderEnter(EntityId id, Collider collider)
        {
            if (entityMapping.TryResolve(collider, out Entity other) && other.Id != id)
            {
                queue.ReportEvent(new InteractionEvent(id, other));
            }
        }

        [Mocked]
        private void HandleColliderStay(EntityId id, Collider collider)
        {
        }

        [Mocked]
        private void HandleColliderExit(EntityId id, Collider collider)
        {
        }
    }
}