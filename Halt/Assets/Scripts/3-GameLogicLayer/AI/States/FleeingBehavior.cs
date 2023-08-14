using Mole.Halt.Utils.Interfaces;
using System.Collections;
using UnityEngine;

namespace Mole.Halt.GameLogicLayer.AI.states
{
    /// <summary>
    /// PROTOTYPE
    /// AI State Machine state. Runs away from the player as long as it is in range. Switches to hibernation after a reasonable time.
    /// </summary>
    public class FleeingBehavior : IEnemyBehave
    {
        private readonly AIStateMachine _controller = null;
        private readonly float _coolDownTime = 10f;

        public FleeingBehavior(AIStateMachine controller)
        {
            _controller = controller;
        }

        public void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                Flee(col);
            }
        }
        public void OnTriggerStay(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                Flee(col);
            }
        }
        public void OnTriggerExit(Collider col)
        {
            // do nothing, wait and chill
        }

        public void OnSoundDetected(Vector3 position)
        {
            Jumpscare();
        }

        public void OnBehaviorActive()
        {
            Jumpscare();
            _controller.StartTimer(_coolDownTime, () => { if (_controller.StatusIs(AIState.retreat)) _controller.SwitchToBehavior(AIState.idle); });
        }

        private void Flee(Collider col)
        {
            throw new System.NotImplementedException();

            //var temp = _controller.transform.position - _controller.Player.transform.position;
            //var temp2 = _controller.transform.position + temp;

            //Debug.LogWarning(temp2);

            //Vector3 oppositeDirection() { return col.ClosestPointOnBounds(_controller.Position); }

            //_controller.NavigateTo(temp2);

            //Jumpscare();
        }

        private void Jumpscare()
        {
            _controller.RestartTimer();
        }

        public void OnDamageReceived(float health)
        {
            Jumpscare();
        }

        public void OnRayCastDetected(RaycastHit hit)
        {
            Jumpscare();
        }
    }
}