using Mole.Halt.DataLayer;

namespace Mole.Halt.GameLogicLayer.Internal
{
    public class InteractionEvent : GameEvent
    {
        public readonly EntityId effector;
        public readonly Entity target;

        public InteractionEvent(EntityId effector, Entity target)
        {
            this.effector = effector;
            this.target = target;
        }

        public override string ToString()
        {
            return $"event-interactor-effector_{effector}-target{target}";
        }
    }
}