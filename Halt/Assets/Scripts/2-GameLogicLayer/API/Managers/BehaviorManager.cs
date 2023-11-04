using Mole.Halt.Utils;

namespace Mole.Halt.GameLogicLayer
{
    public interface BehaviorManager
    {
        public event Callback<Position> OnMoveToPositionRequest;
    }
}
