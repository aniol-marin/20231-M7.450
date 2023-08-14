using UnityEngine;

namespace Mole.Halt.Utils.Interfaces
{
    /// <summary>
    /// Interface for decoupling colliders from their transform
    /// </summary>
    public interface ICollideable
    {
        public void TriggerEnter(Collider other);
        public void TriggerStay(Collider other);
        public void TriggerExit(Collider other);
    }
}