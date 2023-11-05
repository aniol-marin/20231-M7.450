using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Mole.Halt.GameLogicLayer
{
    public class Patrol : Activity
    {
        [Injected] private readonly IQueueManager queue;
        private EntityId owner;
        private int currentIndex;
        private List<Position> positions;
        private bool ccw;

        private int PositionCount => positions.Count;
        private int NextIndex => ccw ? (++currentIndex % PositionCount) : ((--currentIndex + PositionCount) % PositionCount);
        private Position NextPosition => positions[NextIndex];

        public override void Initialize(EntityId owner)
        {
            this.owner = owner;
            ccw = RandomValue.Bool();
        }

        public void SetData(DataExchange exchange)
        {
            WaypointsExchange data = exchange as WaypointsExchange;
            positions = data.positions.ToList();
        }

        public override void Start()
        {
            currentIndex = RandomValue.Int(0, PositionCount);
            Next();
        }

        public override void Stop()
        {
        }

        private void Next()
        {
            queue.ReportEvent(new OrderEvent(OrderType.reach, owner, NextPosition, ControllerType.stateMachine, Next));
        }
    }
}