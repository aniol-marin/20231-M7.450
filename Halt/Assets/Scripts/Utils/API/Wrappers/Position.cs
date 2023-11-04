using UnityEngine;

namespace Mole.Halt.Utils
{
    public readonly struct Position
    {
        private readonly Vector3 value;

        public Vector3 ToVector3 => value;
        public Vector2 ToVector2 => value;

        public Position(float x, float y, float z = 0)
        {
            value = new Vector3(x, y, z);
        }

        public Position(Vector3 vector)
        {
            value = vector;
        }

        public override string ToString()
        {
            return $"[x_{v(value.x)}-y_{v(value.y)}-z_{v(value.z)}]";

            string v(float value) => value.ToPrecision(2);
        }
    }
}