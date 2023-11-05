using Mole.Halt.ApplicationLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.PresentationLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using UnityEngine;


namespace Mole.Halt.Meta
{
    public class CharactersFactory : ICharactersFactory
    {
        [Injected] private readonly BehaviorFactory behaviorProvider;
        [Injected] private readonly CharacterPrefabLoader prefabs;
        [Injected] private readonly InteractionManager interactions;
        [Injected] private readonly IPrefabFactory factory;
        private readonly List<MockBehaviorManager> mockBehaviors = new();

        public CharacterView Instantiate(Character character, InitialCharacterData data, Transform parent)
        {
            CharacterView view = factory.Instantiate(prefabs.GetPrefab(character.CharacterType),parent, true);
            BehaviorManager behavior = behaviorProvider.Get(data.Prototype.ControllerType);
            interactions.AddBehaviorManager(behavior);

            if (behavior is BehaviorStateMachine stateMachine)
            {
                stateMachine.Initialize(character.Id, behaviorProvider.GetWiring(character));
            }
            else if (behavior is MockBehaviorManager mock)
            {
                mock.Initialize(character);
                mockBehaviors.Add(mock);
            }

            view.gameObject.tag = character.EntityType.ToString();
            view.Init(character.Id, data.Speed, data.InitialPosition, data.InitialRotation);
            view.name = $"{character.Label} {character.Id} [FABRICTED by {nameof(CharactersFactory)}]";

            return view;
        }
    }
}