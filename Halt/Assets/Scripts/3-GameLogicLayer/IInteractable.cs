using UnityEngine;

namespace Mole.Halt.Utils.Interfaces
{
    /// <summary>
    /// Interface for managing interactable elements. Use() might be used for player interaction, Trigger() for passive activation instead.
    /// </summary>
    public interface IInteractable
    {
        public void Use();
        public void Trigger();
    }
}