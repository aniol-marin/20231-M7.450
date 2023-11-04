using UnityEngine;

namespace Mole.Halt.ApplicationLayer
{
    public class Level : ControllerNode
    {
        [SerializeField] private Transform characterContainer;

        public Transform CharactersContainer => characterContainer;

        public override void Deinit()
        {
            gameObject.SetActive(false);
        }
    }
}