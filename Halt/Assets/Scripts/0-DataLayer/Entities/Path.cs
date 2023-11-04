using Mole.Halt.Utils;

namespace Mole.Halt.DataLayer
{
    public sealed class Path : Object 
    {
        public Path(string label = null) : base(ObjectType.path, label)
        {
        }

        public Position ClosestPoint(Position location = default)
        {
            new Error("closes point not implemented"); 
            return new Position(0, 0, 0);
        }
    }
}