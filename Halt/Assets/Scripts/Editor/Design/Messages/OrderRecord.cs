using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using UnityEngine;

namespace Mole.Halt.Design
{
    [CreateAssetMenu(menuName = "Mole/Design/Events/Order")]
    public class OrderRecord : Record<OrderEvent>
    {
      [SerializeField] private OrderType type;
      [SerializeField] private BehaviorType strategy;
      [SerializeField] private EntityId effector;
      [SerializeField] private Position target;

        public override GameEvent GameEvent => @event ?? new OrderEvent(type, effector, target, strategy);

        protected override void PopulateInternal(History container)
        {
            @type = @event.type;
            strategy = @event.strategy;
            effector = @event.effector;
            target = @event.target;
        }
    }
}