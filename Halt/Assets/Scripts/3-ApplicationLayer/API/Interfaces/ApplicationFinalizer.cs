using Mole.Halt.Utils;

namespace Mole.Halt.ApplicationLayer
{
    public interface ApplicationFinalizer
    {
        public event Callback OnApplicationQuit;

        public void Quit();
    }
}