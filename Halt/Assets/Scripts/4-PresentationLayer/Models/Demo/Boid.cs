using Mole.Halt.Utils;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Mole.Halt.PresentationLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/Boid")]
    public class Boid : DataAsset
    {
        [SerializeField, Range(0.1f, 10f)] float baseSpeed;
        [SerializeField, Range(0.1f, 10f)] float variableSpeed;
        [SerializeField, Range(0.1f, 10f)] float maxSpeedVariation;
        [SerializeField, Range(0.1f, 10f)] float maxNeighborDistance;
        [SerializeField, Range(0.1f, 10f)] float minNeighborDistance;
        [SerializeField, Range(0.1f, 10f)] float rotationRad;

        public float MaxNeighborDistance => maxNeighborDistance;
        public float MinNeighborDistance => minNeighborDistance;
        public float RotationSpeed => rotationRad;
        public float Speed => UnityEngine.Random.Range(baseSpeed - variableSpeed, baseSpeed + variableSpeed);
        public float MaxSpeedVariation => maxSpeedVariation;

        private void OnValidate()
        {
            Assert.IsTrue(baseSpeed - variableSpeed > 0, "Zero or negative boid speeds are not supported");
        }
    }
}
