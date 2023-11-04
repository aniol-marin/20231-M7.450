using Mole.Halt.Utils;

namespace Mole.Halt.ApplicationLayer
{
    public interface IUserInputService
    {
        event Callback<Position> OnPositionRequested;
        event Callback OnSubmit;
        event Callback<bool> OnToggleConsole;
    }
}