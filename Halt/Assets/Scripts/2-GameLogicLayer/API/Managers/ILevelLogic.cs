using Mole.Halt.Utils;

namespace Mole.Halt.GameLogicLayer
{
    public interface ILevelLogic
    {
        event Callback OnLevelCompleted;

        void ResetData();
    }
}