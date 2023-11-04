using Mole.Halt.Utils;

namespace Mole.Halt.ApplicationLayer
{
    public class SceneRootInitializer : ControllerNode
    {
        [Injected] private readonly SceneFinalizer finalizer;
        [Injected] private readonly SceneInitializer initializer;

        public override void Deinit()
        {
        }

        private void Awake() // Scene entry point
        {
            initializer.InitializeScene(this);
        }

        private void OnDestroy()
        {
            finalizer.FinalizeScene();
        }
    }
}