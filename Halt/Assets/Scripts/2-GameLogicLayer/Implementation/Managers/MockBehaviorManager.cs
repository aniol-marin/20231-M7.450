using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System;
using System.Collections;

namespace Mole.Halt.GameLogicLayer.Internal
{
    public class MockBehaviorManager : BehaviorManager
    {
        [Injected] private readonly TickProvider tickProvider;
        [Injected] private readonly Allocator allocator;
        [Injected] private readonly InteractionManager interactions;
        private Tick update;
        private EntityId id;

        public ControllerType Type => ControllerType.mock;

        public event Callback<Position> OnMoveToPositionRequest = delegate { };

        public void Initialize(Character character)
        {
            id = character.Id;

            interactions.OnInteractionReported += HandleInteraction;
            update = tickProvider.Launch(SelectNextPosition(), $"{nameof(MockBehaviorManager)} [{character}]");
        }

        public void Deinitialize()
        {
            tickProvider.Stop(update);
        }

        private void HandleInteraction(EntityId id, Entity target)
        {
            if (this.id == id)
            {
                new Info($"Mock detected relevant interaction: {id} - {target}");
            }
        }

        private IEnumerator SelectNextPosition()
        {
            Delay delay = allocator.Instantiate<Delay>();
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
                OnMoveToPositionRequest(position);

                yield return delay.Seconds(RandomValue.Float(0, 1) * 15);
            }
        }
    }
}
