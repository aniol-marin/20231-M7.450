using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;

namespace Mole.Halt.PresentationLayer
{
    public class BehaviorStrategy
    {
        [Injected] IQueueManager queueManager;
        private BehaviorType currentBehavior;
        private ControllerType controller;
        private EntityId id;
        Callback<OrderEvent> onOrderReceived;

        public void Initialize(EntityId id, ControllerType controller, Callback<OrderEvent> onOrderReceived)
        {
            this.id = id;
            this.controller = controller;
            this.onOrderReceived = onOrderReceived;

            queueManager.OnEventDequeued += FilterEvents;
        }

        public void Deinitialize()
        {
            id = default;
        }

        private void FilterEvents(GameEvent gameEvent)
        {
            if (gameEvent is OrderEvent order && order.effector == id && acceptedOrder(order.controller))
            {
                // TO DO filter strategy
                onOrderReceived(order);
            }

            bool acceptedOrder(ControllerType controller)
            {
                return controller == this.controller
                    || controller == ControllerType.all;
            }
        }
    }
}