using System.Collections;

namespace Mole.Halt.Utils
{
    public interface Delay
    {
        public IEnumerator Seconds(float seconds);

        public IEnumerator Milliseconds(float ms);
    }
}