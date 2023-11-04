using Mole.Halt.Utils;

namespace Mole.Halt.GameLogicLayer
{
    public interface IQueueManager
    {
        event Callback<GameEvent> OnEventDequeued;

        void ReportEvent(GameEvent gameEvent);
    }
}