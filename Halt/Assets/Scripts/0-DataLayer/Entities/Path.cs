using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Mole.Halt.DataLayer
{
    public sealed class Path : Object 
    {
        private List<Position> positions;

        public override DataExchange Data => new WaypointsExchange() { positions = positions.ToArray() } ;

        public Path(IEnumerable<Position> positions, string label = null) : base(ObjectType.path, label)
        {
            this.positions = positions.ToList();
        }

        public Position ClosestPoint(Position location)
        {
            return positions
                .OrderBy(p => p.Distance(location))
                .Last();
        }

        public Position RandomPoint()
        {
            Position point = positions.ElementAt(RandomValue.Int(0, positions.Count));
            return point;
        }
    }
}