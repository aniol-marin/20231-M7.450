using Mole.Halt.DataLayer;

namespace Mole.Halt.GameLogicLayer
{
    public class Use : Activity
    {
        private EntityId owner;

        public override void Initialize(EntityId owner)
        {
            this.owner = owner;
        }

        public override void Start()
        {
            throw new System.NotImplementedException();
        }

        public override void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}