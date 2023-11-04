using Mole.Halt.ApplicationLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;

namespace Mole.Halt.PresentationLayer
{
    public class PointerInputView : ViewNode, Initializable
    {
        [Injected] private readonly IUserInputService input;
        [Injected] private readonly IQueueManager queue;
        [Injected] private readonly ICensusManager census;

        public void Init()
        {
            input.OnPositionRequested += HandlePositionRequested;
        }

        public void Deinit()
        {
            input.OnPositionRequested -= HandlePositionRequested;
        }

        private void HandlePositionRequested(Position position)
        {
            census
                .Active
                .ForEach(e =>
                {
                    queue.ReportEvent(new OrderEvent(OrderType.reach, e, position, BehaviorType.mock));
                });
        }
    }
}