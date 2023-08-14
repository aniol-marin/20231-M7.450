using Mole.Halt.GameLogicLayer.AI.states;
using Mole.Halt.PresentationLayer;
using Mole.Halt.PresentationLayer.Views;
using Mole.Halt.Utils.Factories;
using Mole.Halt.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Mole.Halt.GameLogicLayer.AI
{
    /// <summary>
    /// State Machine for enemy complex behavior.
    /// 
    /// TODO:
    /// - Migrate and transfer code towads controller
    /// - Implement generic state tree linking.
    /// </summary>
    public class AIStateMachine : MonoBehaviour
    {
        [Inject] public BehaviorFactory _behaviorFactory = null;
        [SerializeField] private AIState _initialStatus = AIState.invalid;
        private IEnemyBehave _currentBehavior = null;
        private Dictionary<AIState, IEnemyBehave> _behaviors = null;

        /// <summary>
        /// Returns the current status without storing variables, through direct dictionary checkup.
        /// </summary>
        public AIState Status
        {
            get
            {
                AIState status = AIState.invalid; //default value for not found (shouldn't happen, but just in case)
                if (_behaviors.Values.Contains(_currentBehavior))
                {
                    foreach (AIState key in _behaviors.Keys)
                    {
                        if (_currentBehavior.Equals(GetState(key)))
                        {
                            status = key;
                            break;
                        }
                    }
                }
                return status;
            }
        }
        /// <summary>
        /// Fast, direct dictionary checkup
        /// </summary>
        /// <param name="status">state to compare against</param>
        /// <returns>current state matches query or not</returns>
        public bool StatusIs(AIState status)
        {
            return _behaviors[status].Equals(_currentBehavior);
        }

        private void Start()
        {
            AttachListeners();
            _behaviors = _behaviorFactory.GetBehaviors(this);
            ResetData();
        }
        private void OnDestroy()
        {
            Cleanup();
            //PresentationBridge.OnSoundEmmited -= HandleLocationSound; // TODO implement alternative
        }
        /// <summary>
        /// Bridge method between AI state and view/controller class
        /// </summary>
        /// <param name="point">destination</param>
        public void NavigateTo(Vector3 point) { }// => _owner.NavigateTo(point);
        /// <summary>
        /// Bridge method between AI state and view/controller class
        /// </summary>
        /// <param name="point">destination</param>
        public void RotateTowards(Vector3 point) { }// => _owner.RotateTowards(point);
        /// <summary>
        /// Bridge method between AI state and view/controller class
        /// </summary>
        public void Patrol() { }// => _owner.Patrol();
        /// <summary>
        /// Bridge method between AI state and view/controller class
        /// </summary>
        public void Scan(Action onScanCompleted = null) { }// => _owner.Scan(onScanCompleted);
        /// <summary>
        /// Bridge method between AI state and view/controller class
        /// </summary>
        public void LaunchAttack() { }// => _owner.Attack();
        /// <summary>
        /// Bridge method between AI state and view/controller class
        /// </summary>
        public void StartTimer(float delay, Action onTimerEnd = null) { }// => _owner.StartTimer(delay, onTimerEnd);
        /// <summary>
        /// Bridge method between AI state and view/controller class
        /// </summary>
        public void RestartTimer() { }// => _owner.RestartTimer();
        /// <summary>
        /// IEnemy behave controlled hot plug. Handles repatch. Prints warning upon requesting switch to the current state in order to facilitate debugging.
        /// </summary>
        /// <param name="state"></param>
        public void SwitchToBehavior(AIState state)
        {
            if (state != Status)
            {
                Cleanup();

                _currentBehavior = GetState(state);

                /*
                Detector.TriggerEnter += _currentBehavior.OnTriggerEnter;
                Detector.TriggerStay += _currentBehavior.OnTriggerStay;
                Detector.TriggerExit += _currentBehavior.OnTriggerExit;
                _owner.OnDamageReceived += _currentBehavior.OnDamageReceived;
                _owner.OnPlayerDetected += _currentBehavior.OnRayCastDetected;
                 */

                _currentBehavior.OnBehaviorActive();
            }
            else
            {
                //Debug.LogWarning($"{_owner.Data.Name} tried to recursively switch to {Status}");
            }
        }
        /// <summary>
        /// Returns the behavior state corresponding to the given key
        /// </summary>
        /// <param name="state">AI state enum</param>
        /// <returns>AI real estate</returns>
        private IEnemyBehave GetState(AIState state)
        {
            _behaviors.TryGetValue(state, out IEnemyBehave behavior);
            return behavior;
        }
        /// <summary>
        /// Filters auditive triggers according to data model before sending them to the state
        /// </summary>
        /// <param name="location">location of the sound</param>
        /// <param name="intensity">intensity of the stimulus</param>
        private void HandleLocationSound(Vector3 location, float intensity)
        {
            /*
            // local function
            float loudness() { return Vector3.Distance(transform.position, location) / intensity; }
            if (loudness() > _owner.AudioThreshold)
            {
                _currentBehavior?.OnSoundDetected(location);
            }
             */
        }
        /// <summary>
        /// Cleans up active action listeners
        /// </summary>
        private void Cleanup()
        {
            if (_currentBehavior != null)
            {
                /*
                Detector.TriggerEnter -= _currentBehavior.OnTriggerEnter;
                Detector.TriggerStay -= _currentBehavior.OnTriggerStay;
                Detector.TriggerExit -= _currentBehavior.OnTriggerExit;
                _owner.OnPlayerDetected -= _currentBehavior.OnRayCastDetected;
                 */
                _currentBehavior = null;
            }
        }

        public void ResetData()
        {
            SwitchToBehavior(_initialStatus);
        }

        public void ClearListeners()
        {
        }

        public void AttachListeners()
        {
        }
    }
}