using Mole.Halt.Utils.Interfaces;
using UnityEngine;

namespace Mole.Halt.GameLogicLayer.AI.states
{
    public class PatrollingBehavior : IEnemyBehave
    {
        /// <summary>
        /// AI State Machine state. Sets the enemy to scan for the player, launches an attack if the player is found, switches to patrolling upon unknown suspicious triggers.
        /// </summary>
        private readonly AIStateMachine _controller;

        public PatrollingBehavior(AIStateMachine enemy)
        {
            _controller = enemy;
        }

        public void OnTriggerEnter(Collider col)
        {
            _controller.SwitchToBehavior(AIState.attack);
        }
        /// <summary>
        /// IEnemiBehave implementation
        /// </summary>
        public void OnTriggerStay(Collider col)
        {
            // do nothing, unless it's the player (which means faulty logic or representation)
            if (col.transform.CompareTag("Player"))
            {
                return;
                throw new System.NotImplementedException();
            }
        }
        /// <summary>
        /// IEnemiBehave implementation
        /// </summary>
        public void OnTriggerExit(Collider col)
        {
            // do nothing, unless it's the player (which means faulty logic or representation)
            if (col.transform.CompareTag("Player"))
            {
                return;
                throw new System.NotImplementedException();
            }
        }

        /// <summary>
        /// IEnemiBehave implementation
        /// </summary>
        public void OnSoundDetected(Vector3 position)
        {
            _controller.SwitchToBehavior(AIState.scan);
        }

        /// <summary>
        /// IEnemiBehave implementation
        /// </summary>
        public void OnBehaviorActive()
        {
            _controller.Patrol();
        }

        /// <summary>
        /// IEnemiBehave implementation
        /// </summary>
        public void OnDamageReceived(float health)
        {
            _controller.SwitchToBehavior(AIState.scan);
        }

        /// <summary>
        /// IEnemiBehave implementation
        /// </summary>
        public void OnRayCastDetected(RaycastHit hit)
        {
            if (hit.transform.CompareTag("Player"))
            {
                _controller.SwitchToBehavior(AIState.attack);
            }
            // else ignore
        }
    }
}