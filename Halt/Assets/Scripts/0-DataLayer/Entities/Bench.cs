using Mole.Halt.Utils;

namespace Mole.Halt.DataLayer
{
    public sealed class Bench : Object 
    {
        private readonly DataExchange data;

        public override DataExchange Data => data;

        public Bench(DataExchange data, string label = null) : base(ObjectType.bench, label)
        {
            this.data = data;
        }
    }
}