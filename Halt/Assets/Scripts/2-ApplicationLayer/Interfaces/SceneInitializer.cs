using System;

namespace Mole.Halt.ApplicationLayer
{
    public interface SceneInitializer
    {
        public event Action OnSceneInitialized;

        public void InitializeScene();
    }
}