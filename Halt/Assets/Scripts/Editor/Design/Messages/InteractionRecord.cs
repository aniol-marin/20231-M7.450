using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using UnityEngine;
using Object = Mole.Halt.DataLayer.Object;

namespace Mole.Halt.Design
{
    [CreateAssetMenu(menuName = "Mole/Design/Events/Interaction")]
    public class InteractionRecord : Record<InteractionEvent>
    {
        [SerializeField] private SerializedCharacter characterTarget;
        [SerializeField] private SerializedObject objectTarget;
        [SerializeField] private EntityId effector;

        public override GameEvent GameEvent => @event ?? new InteractionEvent(effector, characterTarget.Entity);

        protected override void PopulateInternal(History container)
        {
            effector = @event.effector;

            if (@event.target is Character character)
            {
                characterTarget = CreateInstance<SerializedCharacter>();
                characterTarget.Populate(character);
                characterTarget.Store(container);
                container.Add(characterTarget);
            }
            else if (@event.target is Object @object)
            {
                objectTarget = CreateInstance<SerializedObject>();
                objectTarget.Populate(@object);
                objectTarget.Store(container);
                container.Add(objectTarget);
            }
            else
            {
                new Warning($"trying to record an unkown entity: {@event.target.GetType()}");
            }

        }
    }
}