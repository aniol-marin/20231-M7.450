using Mole.Halt.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Mole.Halt.PresentationLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/Flock")]
    public class Flock : DataAsset, IFlock
    {
        public Vector3 target;
        public Vector3 center;
        public Vector3 heading;
        public InsectView[] boids;

        public Vector3 Center => center;
        public Vector3 Heading => heading;

        public delegate float DistanceDelegate(InsectView v1, InsectView v2);

        DistanceDelegate @delegate;

        public IEnumerable<InsectView> Boids => boids;


        public void SetDistanceDelegate(DistanceDelegate distanceDelgate)
        {
            @delegate = distanceDelgate;
        }

        public float GetDistance(InsectView v1, InsectView v2)
        {
            return @delegate(v1, v2);
        }
    }
}