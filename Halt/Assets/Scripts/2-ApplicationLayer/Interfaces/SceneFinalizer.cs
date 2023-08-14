using System;

namespace Mole.Halt.ApplicationLayer
{
    public interface SceneFinalizer
    {
        public event Action OnSceneEnded;

        public void FinalizeScene();
    }
}