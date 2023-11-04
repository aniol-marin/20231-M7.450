using Mole.Halt.Utils;

namespace Mole.Halt.PresentationLayer
{
    public class Navigation
    {
        private ScreenId previousScreen;

        public event Callback<ScreenId> OnEnteringScreen = delegate { };
        public event Callback OnLeavingCurrentScreen = delegate { };

        public void NavigateTo(ScreenId screen)
        {
            if (previousScreen != default)
            {
                OnLeavingCurrentScreen();
            }
            previousScreen = screen;

            OnEnteringScreen(screen);
        }
    }
}