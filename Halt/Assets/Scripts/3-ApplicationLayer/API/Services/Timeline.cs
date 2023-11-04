using Mole.Halt.Utils;
using System;

namespace Mole.Halt.ApplicationLayer
{
    public class Timeline
    {
        private State status;

        public event Callback OnPlay = delegate { };
        public event Callback OnStop = delegate { };
        public event Callback<bool> OnPauseToggled = delegate { };

        public Timeline()
        {
            status = State.stopped;
        }

        public void Play()
        {
            status = State.playing;

            OnPlay();
        }

        public void TogglePause()
        {
            status = status switch
            {
                State.stopped => status,
                State.paused => State.playing,
                State.playing => State.paused,
                _ => throw new NotImplementedException(),
            };

            bool paused = status == State.paused;
            bool toggled = paused || status == State.playing;
            if (toggled)
            {
                OnPauseToggled(paused);
            }
        }

        public void Stop()
        {
            status = State.stopped;
            OnStop();
        }

        private enum State
        {
            stopped = default,
            playing,
            paused,
        }
    }
}