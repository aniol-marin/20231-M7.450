using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.PresentationLayer;
using Mole.Halt.Utils;
using UnityEngine;


namespace Mole.Halt.Meta
{
    public class CharactersFactory : ICharactersFactory
    {
        [Injected] private readonly BehaviorFactory behaviorProvider;
        [Injected] private readonly CharacterPrefabLoader prefabs;
        [Injected] private readonly InteractionManager interactions;
        [Injected] private readonly IPrefabFactory factory;

        public CharacterView Instantiate(Character character, ControllerType controller, Vector3 position, Quaternion rotation, Transform parent)
        {
            CharacterView view = factory.Instantiate(prefabs.GetPrefab(character.CharacterType),parent, true);
            BehaviorManager behavior = behaviorProvider.Get(controller);
            interactions.AddBehaviorManager(behavior);

            if (behavior is BehaviorStateMachine stateMachine)
            {
                stateMachine.Initialize(character.Id, behaviorProvider.GetWiring(character));
            }

            view.gameObject.tag = character.EntityType.ToString();
            view.Init(character.Id, behavior, position, rotation);
            view.name = $"{character.Label} {character.Id} [FABRICTED by {nameof(CharactersFactory)}]";

            return view;
        }
    }
}