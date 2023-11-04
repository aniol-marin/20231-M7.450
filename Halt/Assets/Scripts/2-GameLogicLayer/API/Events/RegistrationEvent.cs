using Mole.Halt.DataLayer;

namespace Mole.Halt.GameLogicLayer.Internal
{
    public class RegistrationEvent : GameEvent
    {
        public readonly Entity entity;

        public RegistrationEvent(Entity entity)
        {
            this.entity = entity;
        }

        public override string ToString()
        {
            return $"event-registration-{entity}";
        }
    }
}