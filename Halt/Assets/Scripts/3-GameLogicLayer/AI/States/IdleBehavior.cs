using Mole.Halt.Utils.Interfaces;
using UnityEngine;

namespace Mole.Halt.GameLogicLayer.AI.states
{
    /// <summary>
    /// AI State Machine state. Stays idle until triggered. If attacked it will scan for the source, otherwise it will transition to an alert state.
    /// </summary>
    public class IdleBehavior : IEnemyBehave
    {
        private readonly AIStateMachine _controller;

        public IdleBehavior(AIStateMachine enemy)
        {
            _controller = enemy;
        }

        public void OnTriggerEnter(Collider col)
        {
            // do nothing
        }
        public void OnTriggerStay(Collider col)
        {
            // do nothing
        }
        public void OnTriggerExit(Collider col)
        {
            // do nothing
        }

        public void OnSoundDetected(Vector3 position)
        {
            _controller.SwitchToBehavior(AIState.alert);
        }

        public void OnBehaviorActive()
        {
            // do nothing
        }

        public void OnDamageReceived(float health)
        {
            _controller.SwitchToBehavior(AIState.scan);
        }

        public void OnRayCastDetected(RaycastHit hit)
        {
            _controller.SwitchToBehavior(AIState.alert);
        }
    }
}