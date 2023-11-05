using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using System;
using System.Collections;

namespace Mole.Halt.GameLogicLayer
{
    public class Wander : Activity
    {
        [Injected] private readonly TickProvider tickProvider;
        [Injected] private readonly IQueueManager queue;
        [Injected] private readonly Delay delay;
        private EntityId owner;
        private Tick update;

        public override void Initialize(EntityId owner)
        {
            this.owner = owner;
        }

        public override void Start()
        {
            update = tickProvider.Launch(SelectNextPosition(), $"{nameof(Wander)} [{owner}]");
        }

        public override void Stop()
        {
            tickProvider.Stop(update);
        }

        private IEnumerator SelectNextPosition()
        {
            Position position;
            float size = 5;
            float offset = size / 2f;
            float exclusion = 35;

            float p()
            {
                float v = RandomValue.Float(0, size) * size - offset;
                return (v > 0)
                    ? Math.Max(v, exclusion)
                    : Math.Min(v, -exclusion);
            }


            for (; ; )
            {
                position = new Position(p(), 0, p());
                queue.ReportEvent(new OrderEvent(OrderType.reach, owner, position, ControllerType.all));

                yield return delay.Seconds(RandomValue.Float(0, 1) * 15);
            }
        }
    }
}