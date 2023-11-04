using Mole.Halt.Utils;
using System.Collections;
using System.Collections.Generic;

namespace Mole.Halt.GameLogicLayer.Internal
{
    public class QueueManager : Initializable, IQueueManager
    {
        [Injected] private readonly TickProvider coroutine;
        private readonly Queue<GameEvent> queue = new();
        private Tick update;

        public event Callback<GameEvent> OnEventDequeued = delegate { };

        public void Init()
        {
            update = coroutine.Launch(Update(), this);
        }

        public void Deinit()
        {
            coroutine.Stop(update);
        }

        public void ReportEvent(GameEvent gameEvent)
        {
            queue.Enqueue(gameEvent);
        }


        private IEnumerator Update()
        {
            for (; ; )
            {
                while (!queue.Empty())
                {
                    GameEvent e = queue.Dequeue();
                    OnEventDequeued(e);
                }
                yield return null;
            }
        }
    }

}
