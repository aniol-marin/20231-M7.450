using Mole.Halt.Utils;

namespace Mole.Halt.ApplicationLayer
{
    public interface Resetter
    {
        public event Callback OnSceneReset;
        public event Callback OnLevelReset;
    }
}