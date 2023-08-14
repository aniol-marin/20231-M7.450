using Mole.Halt.Utils.Interfaces;
using UnityEngine;

namespace Mole.Halt.GameLogicLayer.AI.states
{
    /// <summary>
    /// AI State Machine state. Sets the enemy on alert, may move closer to unusual auditive and visual triggers. Scans upon suspicious presence. Switches to attack upon player sight.
    /// </summary>
    public class AlertBehavior : IEnemyBehave
    {
        private readonly AIStateMachine _controller;

        public AlertBehavior(AIStateMachine controller)
        {
            _controller = controller;
        }
            
        public void OnTriggerEnter(Collider col)
        {
            if (col.transform.CompareTag("Player"))
            {
                _controller.SwitchToBehavior(AIState.scan);
            }
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
            _controller.NavigateTo(position);
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
            if (hit.transform.CompareTag("Player"))
            {
                _controller.SwitchToBehavior(AIState.attack);
            }
            // else ignore
        }
    }
}