using Mole.Halt.Utils;

namespace Mole.Halt.ApplicationLayer
{
    public interface SceneInitializer
    {
        public event Callback OnSceneInitialized;

        public void InitializeScene(NodeClass root);
        public void TryInitialize(Initializable instance);
    }
}