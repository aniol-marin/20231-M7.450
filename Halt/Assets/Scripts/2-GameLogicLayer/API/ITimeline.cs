using Mole.Halt.Utils;

namespace Mole.Halt.GameLogicLayer
{
    public interface ITimeline
    {
        event Callback<bool> OnPauseToggled;
        event Callback OnPlay;
        event Callback OnStop;

        void Play();
        void Stop();
        void TogglePause();
    }
}