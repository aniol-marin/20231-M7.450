using Mole.Halt.Utils;

namespace Mole.Halt.ApplicationLayer
{
    public interface SceneChanger
    {
        public event Callback OnSceneChange;

        public void ChangeScene(SceneId scene);
    }
}