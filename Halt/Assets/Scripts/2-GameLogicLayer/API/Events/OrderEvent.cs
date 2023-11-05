using Mole.Halt.DataAccessLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System;

namespace Mole.Halt.GameLogicLayer.Internal
{
    public class OrderEvent : GameEvent
    {
        public readonly DataExchange data;
        public readonly Type activity;
        public readonly OrderType type;
        public readonly ControllerType controller;
        public readonly EntityId effector;
        public readonly Position target;
        public readonly Callback onCompleted;

        public OrderEvent(OrderType type, EntityId effector, Position target, ControllerType controller, Callback onCompleted = null)
        {
            this.type = type;
            this.effector = effector;
            this.target = target;
            this.controller = controller;
            this.onCompleted = onCompleted;
        }

        public override string ToString()
        {
            return $"event-order-{type}-strategy_{controller}-effector_{effector}-target_{target}";
        }
    }

    public enum OrderType
    {
        Invalid = default,
        reach,
        use,
    }
}