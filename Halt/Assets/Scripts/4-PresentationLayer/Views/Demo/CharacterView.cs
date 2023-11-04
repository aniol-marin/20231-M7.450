using Mole.Halt.ApplicationLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Mole.Halt.PresentationLayer
{
    public class CharacterView : ViewNode
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Collider[] colliders;
        [SerializeField] private ColliderView detectionArea;
        [SerializeField] private ColliderView interactionArea;
        private BehaviorManager behavior;
        public EntityId id;

        public IEnumerable<Collider> Colliders => colliders;

        public void Init(
            EntityId id,
            BehaviorManager behavior,
            Vector3 initialPosition,
            Quaternion initialRotation)
        {
            this.id = id;
            this.behavior = behavior;
            transform.SetPositionAndRotation(initialPosition, initialRotation);

            behavior.OnMoveToPositionRequest += HandleMoveTo;
        }

        public void Init(Initializer values)
        {
            detectionArea.Init(() => id, values.onNewColliderDetected, values.onSameColliderDetected, values.onColliderContactLost);
        }

        private void OnDisable()
        {
            if (behavior != null)
            {
                behavior.OnMoveToPositionRequest -= HandleMoveTo;
            }
        }

        public void GiveOrder(OrderEvent order)
        {
            HandleMoveTo(order.target);

        }

        private void HandleMoveTo(Position position)
        {
            agent.destination = position.ToVector3;
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