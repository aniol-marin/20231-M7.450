using Mole.Halt.ApplicationLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using Pada1.BBEditor.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Mole.Halt.PresentationLayer
{
    public class CharacterView : ViewNode
    {
        private const float DELAY_SECONDS = 0.1f;
        private const float PROXIMITY_THRESHOLD = 1.0f;

        [Injected] private readonly Delay delay;
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Collider[] colliders;
        [SerializeField] private ColliderView detectionArea;
        [SerializeField] private ColliderView interactionArea;
        [SerializeField] private int justiceId;
        public EntityId id;

        public IEnumerable<Collider> Colliders => colliders;

        public override EntityId Id => id;
        public int JusticeId => justiceId;

        public void Init(
            EntityId id,
            float speed,
            Vector3 initialPosition,
            Quaternion initialRotation)
        {
            this.id = id;
            transform.SetPositionAndRotation(initialPosition, initialRotation);

            agent.speed = speed;
        }

        public void Init(Initializer values)
        {
            detectionArea.Init(() => id, values.onNewColliderDetected, values.onSameColliderDetected, values.onColliderContactLost);
        }

        public void SetChaseTarget(Transform transform)
        {
            StartCoroutine(Chase(transform));
        }

        IEnumerator Chase(Transform transform)
        {
            yield break;
        }

        public void GiveOrder(OrderEvent order)
        {
            switch (order.type)
            {
                case OrderType.reach:
                    HandleMoveTo(order.target, order.onCompleted);
                    break;
                default:
                    new Error($"entity {id} received invalid order: {order}");
                    break;
            }
        }

        private void HandleMoveTo(Position position, Callback onComplete)
        {
            agent.destination = position.ToVector3;
            StartCoroutine(CheckForDestination(onComplete));
        }

        private IEnumerator CheckForDestination(Callback onComplete = null)
        {
            while (Vector3.Distance(transform.position, agent.destination) > PROXIMITY_THRESHOLD)
            {
                yield return delay.Seconds(DELAY_SECONDS);
            }

            onComplete?.Invoke();

            yield break;
        }

        public readonly struct Initializer
        {
            public readonly Callback<EntityId, Collider> onNewColliderDetected;
            public readonly Callback<EntityId, Collider> onSameColliderDetected;
            public readonly Callback<EntityId, Collider> onColliderContactLost;

            public Initializer(
                Callback<EntityId, Collider> onNewColliderDetected,
                Callback<EntityId, Collider> onSameColliderDetected,
                Callback<EntityId, Collider> onColliderContactLost)
            {
                this.onNewColliderDetected = onNewColliderDetected;
                this.onSameColliderDetected = onSameColliderDetected;
                this.onColliderContactLost = onColliderContactLost;
            }
        }
    }
}