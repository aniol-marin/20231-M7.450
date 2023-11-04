using Mole.Halt.DataAccessLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using UnityEngine;

namespace Mole.Halt.Design
{
    [CreateAssetMenu(menuName = "Mole/Design/Events/Registration")]
    public class RegistrationRecord : Record<RegistrationEvent>
    {
        [SerializeField] private EntityFilter filter;
        [SerializeField] private Entity entity;
        [SerializeField] private uint entityId;

        public override GameEvent GameEvent => @event ?? new RegistrationEvent(entity);

        protected override void PopulateInternal(History container)
        {
            entity = @event.entity;
            entityId = (uint)entity.Id;
        }
    }
}