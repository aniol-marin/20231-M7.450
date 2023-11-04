using Mole.Halt.DataAccessLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.Utils;

namespace Mole.Halt.GameLogicLayer.Internal
{
    public class OrderEvent : GameEvent
    {
        public readonly Activity activity;
        public readonly OrderType type;
        public readonly BehaviorType strategy;
        public readonly EntityId effector;
        public readonly Position target;

        public OrderEvent(OrderType type, EntityId effector, Position target, BehaviorType strategy = default)
        {
            this.type = type;
            this.effector = effector;
            this.target = target;
            this.strategy = strategy;
        }

        public OrderEvent(IActivity activityWrapper, EntityId effector)
        {
            this.activity = activityWrapper.activity;
            this.effector = effector;
        }


        public override string ToString()
        {
            return $"event-order-{type}-strategy_{strategy}-effector_{effector}-target_{target}";
        }
    }

    public enum OrderType
    {
        Invalid = default,
        reach,
        use,
    }
}