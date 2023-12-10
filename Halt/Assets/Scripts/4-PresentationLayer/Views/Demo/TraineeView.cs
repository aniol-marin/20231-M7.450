using Mole.Halt.ApplicationLayer;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

namespace Mole.Halt.PresentationLayer
{
    public class TraineeView : ViewNode
    {
        [SerializeField] NavMeshAgent agent;
        Vector3 offset;

        private void OnValidate()
        {
            Assert.IsNotNull(agent);
        }

        private void Awake()
        {
            agent.speed = Random.Range(agent.speed, agent.speed + 1f);
            agent.angularSpeed = Random.Range(agent.angularSpeed, agent.angularSpeed + 30f);
            agent.acceleration = Random.Range(agent.acceleration, agent.acceleration + 1f);
        }

        public void SetOffset(Vector3 offset)
        {
            this.offset = offset;
        }

        public void SetNewTarget(Vector3 position, Quaternion rotation)
        {
            agent.destination = position + offset;
            // TO DO rotation
        }
    }
}