using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Mole.Halt.PresentationLayer
{
    public class FlockManager : ViewNode
    {
        [SerializeField] Boid data;
        [SerializeField] Flock flock;
        [SerializeField] InsectView[] insects;
        readonly Dictionary<KeyValuePair<InsectView, InsectView>, float> distances = new();

        private void OnValidate()
        {
            Assert.IsNotNull(data, "FlockManager has no linked data");
            Assert.IsNotNull(flock, "FlockManager has no linked flock");
            Assert.IsTrue(insects.Any(), "flock has no elements");
            Assert.IsFalse(insects.Any(i => i == null), "some flock elements are null");
        }

        private void Awake()
        {
            flock.boids = insects;
            insects.ForEach(i => i.Init(data));

            flock.SetDistanceDelegate(HandleDistanceRequest);
            flock.target = new Vector3(-17.4780006f, 0.771000028f, 12.2019997f);
        }

        private float HandleDistanceRequest(InsectView v1, InsectView v2)
        {
            float result;

            if (v1.Hash < v2.Hash)
            {
                (v1, v2) = (v2, v1);
            }
            KeyValuePair<InsectView, InsectView> key = KeyValuePair.Create(v1, v2);

            if (!distances.TryGetValue(key, out result))
            {
                result = v1.GetPosition().Distance(v2.GetPosition());
                new Error($"requesting {v1.Hash} and {v2.Hash} ({result})");
            }
            else
            {
                new Error($"<color=green>already cached {v1.Hash} and {v2.Hash} ({result})</color>");

            }

            return result;
        }

        private void Update()
        {
            distances.Clear();
            
            float factor = 1f / insects.Length;

            flock.center = insects
                .Select(i => i.GetPosition())
                .Select(p => p.ToVector3 * factor)
                .Aggregate((v1, v2) => v1 + v2);


            flock.heading = flock.target - flock.center;

            insects.ForEach(i => i.UpdateBoid(flock));
        }
    }
}
