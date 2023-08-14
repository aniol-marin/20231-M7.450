using Mole.Halt.Utils.Interfaces;
using System;
using UnityEngine;

namespace Mole.Halt.GameLogicLayer.AI.states
{
    /// <summary>
    /// AI State Machine state. Sets the enemy to scan for the player, launches an attack if found within the relevant time frame, switches back to patrolling otherwise.
    /// </summary>
    public class ScanningBehaviour : IEnemyBehave
    {
        private readonly AIStateMachine _controller;
        private Action _onScanningEnded = null;

        public ScanningBehaviour(AIStateMachine enemy)
        {
            _controller = enemy;
        }
        /// <summary>
        /// IEnemiBehave implementation
        /// </summary>
        public void OnBehaviorActive()
        {
            _onScanningEnded += () => _controller.SwitchToBehavior(AIState.patrol);
            _controller.StartTimer(5f, _onScanningEnded);
            _controller.Scan();
        }

        /// <summary>
        /// IEnemiBehave implementation
        /// </summary>
        public void OnDamageReceived(float health)
        {
            _controller.RestartTimer();
        }

        /// <summary>
        /// IEnemiBehave implementation
        /// </summary>
        public void OnRayCastDetected(RaycastHit hit)
        {
            if (hit.transform != null && hit.transform.CompareTag("Player"))
            {
                _onScanningEnded = null;
                _controller.SwitchToBehavior(AIState.attack);
            }
        }

        /// <summary>
        /// IEnemiBehave implementation
        /// </summary>
        public void OnSoundDetected(Vector3 position)
        {
            _controller.RestartTimer();
            _controller.NavigateTo(position);
        }

        /// <summary>
        /// IEnemiBehave implementation
        /// </summary>
        public void OnTriggerEnter(Collider col)
        {
            //do nothing
        }

        /// <summary>
        /// IEnemiBehave implementation
        /// </summary>
        public void OnTriggerExit(Collider col)
        {
            //do nothing
        }

        /// <summary>
        /// IEnemiBehave implementation
        /// </summary>
        public void OnTriggerStay(Collider col)
        {
            //do nothing
        }
    }
}
