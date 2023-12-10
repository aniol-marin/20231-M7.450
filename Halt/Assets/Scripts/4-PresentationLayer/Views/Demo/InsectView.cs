using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Mole.Halt.PresentationLayer
{
    public class InsectView : ViewNode
    {
        Boid data;
        float speed;
        int hash;

        public int Hash => hash;
        float MaxSpeed => speed + data.MaxSpeedVariation;

        public void Init(Boid data)
        {
            this.data = data;
            speed = data.Speed;
            hash = GetHashCode();
        }

        private void Start()
        {
            Assert.IsTrue(speed > 0, $"[{transform.parent.name}/{name}] insect view not initialized");
        }

        public void UpdateBoid(IFlock flock)
        {
            Vector3 translation = GetTranslation(flock);

            Translate(translation);
            Rotate(translation);
        }

        void Translate(in Vector3 translation)
        {
            transform.Translate(translation.x, translation.y, translation.z);
        }

        void Rotate(in Vector3 translation)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(translation),
                data.RotationSpeed * Time.deltaTime);
        }

        Vector3 GetTranslation(in IFlock flock)
        {
            Vector3 translation;
            
            Vector3 direction = GetDirection(flock);
            if (direction.magnitude > MaxSpeed)
            {
                translation = direction * (MaxSpeed / direction.magnitude);
            }
            else
            {
                translation = direction;
            }

            return speed
               * Time.deltaTime
               * translation;
        }

        Vector3 GetDirection(IFlock flock) => transform.forward
            + flock.Heading
            + CalculateAvoidance(flock)
            + CalculateCentering(flock);

        Vector3 CalculateCentering(IFlock flock)
        {
            Vector3 own = GetPosition().ToVector3;
            Vector3 center = flock.Center;

            return center - own;
        }

        Vector3 CalculateAvoidance(IFlock flock)
        {
            Vector3 avoidance = Vector3.zero;

            IEnumerable<Position> distances = flock.Boids
                  .Except(new[] { this })
                  .Select(b => b.GetPosition())
                  .Where(p => data.MaxNeighborDistance > Vector3.Distance(this.GetPosition().ToVector3, p.ToVector3));

            if (distances.Any())
            {
                int neighborCount = distances.Count();

                avoidance += distances
                .Select(NeighborAvoidance)
                .Aggregate((d1, d2) => d1 + d2 * data.MaxNeighborDistance / neighborCount);
            }

            return avoidance;
        }

        Vector3 NeighborAvoidance(Position position)
        {
            Vector3 avoidance;
            float distance = position.Distance(GetPosition());

            if (data.MinNeighborDistance > distance)
            {
                avoidance = GetPosition().ToVector3 - position.ToVector3;
            }
            else
            {
                avoidance = Vector3.zero;
            }

            return avoidance;
        }
    }
}