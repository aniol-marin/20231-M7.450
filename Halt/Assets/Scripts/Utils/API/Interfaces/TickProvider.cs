using System.Collections;

namespace Mole.Halt.Utils
{
    public interface TickProvider
    {
        public Tick Launch(IEnumerator routine, object requester);

        public void Stop(Tick tick);
    }
}