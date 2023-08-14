using System;

namespace Mole.Halt.ApplicationLayer
{
    public interface ApplicationFinalizer
    {
        public event Action OnApplicationQuit;

        public void Quit();
    }
}