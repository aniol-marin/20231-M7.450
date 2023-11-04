using Mole.Halt.ApplicationLayer;

namespace Mole.Halt.PresentationLayer
{
    public abstract class MenuScreen : ControllerNode
    {
        abstract public ScreenId ScreenType { get; }

        public virtual void Enter()
        {
            ToggleVisuals(true);
        }

        public virtual void Leave()
        {
            ToggleVisuals(false);
        }
    }
}