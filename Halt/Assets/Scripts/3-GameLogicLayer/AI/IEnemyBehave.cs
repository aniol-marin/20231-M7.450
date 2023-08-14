using UnityEngine;

namespace Mole.Halt.Utils.Interfaces
{
    /// <summary>
    /// Interface for binding AI states to the AI state machine
    /// 
    /// TODO
    /// - Couple a version of the interface with the ColliderView in order to decouple it from the transform with the Collider
    /// </summary>
    public interface IEnemyBehave
    {
        void OnBehaviorActive();

        void OnTriggerEnter(Collider col);
        void OnTriggerStay(Collider col);
        void OnTriggerExit(Collider col);

        void OnSoundDetected(Vector3 position);
        void OnDamageReceived(float health);
        void OnRayCastDetected(RaycastHit hit);
    }
}