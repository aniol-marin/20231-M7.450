using UnityEngine;

namespace Mole.Halt.Utils
{
    public class MonoBehaviorTick : Tick
    {
        public readonly string requester;
        public readonly Coroutine coroutine;

        public string Requester => requester;

        public MonoBehaviorTick(string requester, Coroutine coroutine)
        {
            this.requester = requester;
            this.coroutine = coroutine;
        }
    }
}