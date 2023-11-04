using Mole.Halt.ApplicationLayer.Internal;
using Mole.Halt.Utils;

namespace Mole.Halt.ApplicationLayer
{
    public abstract class ControllerNode : NodeClass, Initializable
    {
        [Injected] private readonly SceneInitializer initializer;
        [Injected] private readonly InitializablesProvider initializables;

        public virtual void Init()
        {
            initializables
                .FetchNodeTree(this)
                .ForEach(i => initializer.TryInitialize(i));
        }

        abstract public void Deinit();
    }
}