using System;

namespace Mole.Halt.ApplicationLayer
{
    public interface SceneChanger
    {
        public event Action OnSceneChange;

        public void ChangeScene(SceneId scene);
    }
}