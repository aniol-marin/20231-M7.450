using Mole.Halt.Utils;

namespace Mole.Halt.ApplicationLayer
{
    public interface SceneFinalizer
    {
        public event Callback OnSceneEnded;

        public void FinalizeScene();
    }
}