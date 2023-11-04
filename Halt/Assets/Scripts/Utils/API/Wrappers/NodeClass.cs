using UnityEngine;

namespace Mole.Halt.Utils
{
    public abstract class NodeClass : MonoBehaviour
    {
        public void ToggleVisuals(bool state)
        {
            gameObject.SetActive(state);
        }

        public Position GetPosition()
        {
            return new Position(transform.position);
        }
    }
}