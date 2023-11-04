using UnityEngine;

namespace Mole.Halt.Utils
{
    public static class PositionExtensions
    {
        public static Position ToPosition(this Vector3 position)
        {
            return new Position(position);
        }
    }
}