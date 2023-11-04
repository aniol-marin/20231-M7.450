using System.Collections;
using UnityEngine;

namespace Mole.Halt.Utils
{
    public class MonoBehaviorDelay : Delay
    {
        public IEnumerator Seconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            yield break;
        }
        public IEnumerator Milliseconds(float ms)
        {
            yield return new WaitForSeconds(ms / 1000);
            yield break;
        }
    }
}