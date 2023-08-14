using Mole.Halt.Utils.Interfaces;
using System;
using UnityEngine;

namespace Mole.Halt.GameLogicLayer.AI.states
{
    /// <summary>
    /// AI State Machine state. Attacks the player as long as it is in range. Switches to alert if it loses track. After a delay, if not found while in alert, will switch to patrol. May retreat if heavily damaged.
    /// </summary>
    public class HostileBehavior : IEnemyBehave
    {
        private readonly AIStateMachine _controller;
        private Action _onHostilityEnded = null;

        public HostileBehavior(AIStateMachine controller)
        {
            _controller = controller;
        }

        public void OnTriggerEnter(Collider col)
        {
            _controller.RestartTimer();
            //_controller.RotateTowards(_controller.Player.Position);
            _controller.LaunchAttack();
        }
        public void OnTriggerStay(Collider col)
        {
            _controller.RestartTimer();
            //_controller.RotateTowards(_controller.Player.Position);
            _controller.LaunchAttack();
        }
        public void OnTriggerExit(Collider col)
        {
            _controller.RestartTimer();
            _controller.NavigateTo(col.gameObject.transform.position);
            _controller.SwitchToBehavior(AIState.alert);
        }

        public void OnSoundDetected(Vector3 position)
        {
            // do nothing
        }

        public void OnBehaviorActive()
        {
            _onHostilityEnded += () => { if (_controller.StatusIs(AIState.alert)) _controller.SwitchToBehavior(AIState.patrol); };

            _controller.StartTimer(10f, _onHostilityEnded);
        }

        public void OnDamageReceived(float health)
        {
            if (health < 20f)
            {
                _onHostilityEnded = null;
                _controller.SwitchToBehavior(AIState.retreat);
            }
        }

        public void OnRayCastDetected(RaycastHit hit)
        {
            // do nothing
        }
    }
}