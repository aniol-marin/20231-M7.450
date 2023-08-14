using System;

namespace Mole.Halt.ApplicationLayer.Interfaces
{
    public interface Resetter
    {
        public event Action OnSceneReset;
        public event Action OnLevelReset;
    }
}