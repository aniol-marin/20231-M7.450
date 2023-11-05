using UnityEngine;

namespace Mole.Halt.Utils
{
    public static class PositionExtensions
    {
        public static Position ToPosition(this Vector3 position)
        {
            return new Position(position);
        }
        public static float Distance(this Position first, Position second)
        {
            return Vector3.Distance(first.ToVector3, second.ToVector3);
        }
    }
}