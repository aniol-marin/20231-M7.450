using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;

namespace Mole.Halt.PresentationLayer
{
    public class MenuSceneController : ControllerNode
    {
        [Injected] private readonly Navigation navigation;
        [Injected] private readonly SceneInitializer initializer;

        public override void Init()
        {
            base.Init();

            initializer.OnSceneInitialized += MockFirstScreenNavigation;
        }

        public override void Deinit()
        {

        }

        private void MockFirstScreenNavigation()
        {
            navigation.NavigateTo(ScreenId.Main);
        }
    }
}