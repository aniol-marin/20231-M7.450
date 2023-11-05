using Mole.Halt.DataLayer;

namespace Mole.Halt.GameLogicLayer
{
    public class Rest : Activity
    {
        private EntityId owner;

        public override void Initialize(EntityId owner)
        {
            this.owner = owner;
        }

        public override void Start()
        {
        }

        public override void Stop()
        {
        }
    }
}