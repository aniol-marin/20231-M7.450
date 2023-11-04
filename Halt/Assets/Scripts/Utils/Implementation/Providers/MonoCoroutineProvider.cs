using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mole.Halt.Utils.Internal
{
    using ImplementedTick = MonoBehaviorTick;

    public class MonoCoroutineProvider : MonoBehaviour, TickProvider
    {
        private List<ImplementedTick> activeCoroutines = new();

        public IEnumerable<ImplementedTick> ActiveCoroutines => activeCoroutines;

        public Tick Launch(IEnumerator routine, object requester)
        {
            ImplementedTick request = new ImplementedTick(requester.ToString(), StartCoroutine(routine));
            activeCoroutines.Add(request);
            return request;
        }

        public void Stop(Tick tick)
        {
            ImplementedTick monoTick = tick as ImplementedTick;
            StopCoroutine(monoTick.coroutine);
            ImplementedTick entry = activeCoroutines.Find(r => r.coroutine.Equals(tick));
            activeCoroutines.Remove(entry);
        }

        public void OnDestroy()
        {
            StopAllCoroutines();
            if (!activeCoroutines.Empty())
            {
                activeCoroutines.ForEach(c => new Warning($"<color=orange>{c.requester}</color> didn't clean up its coroutine"));
                activeCoroutines.Clear();
            }
        }
    }
}