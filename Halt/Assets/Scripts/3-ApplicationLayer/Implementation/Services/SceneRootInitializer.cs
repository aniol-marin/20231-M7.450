using Mole.Halt.GameLogicLayer;
using Mole.Halt.Utils;

namespace Mole.Halt.ApplicationLayer
{
    public class SceneRootInitializer : ControllerNode
    {
        [Injected] private readonly SceneFinalizer finalizer;
        [Injected] private readonly SceneInitializer initializer;
        [Injected] private readonly EntityManager test;

        public override void Deinit()
        {
        }

        private void Awake() // Scene entry point
        {
            test.Init();
            initializer.InitializeScene(this);
        }

        private void OnDestroy()
        {
            finalizer.FinalizeScene();
        }
    }
}