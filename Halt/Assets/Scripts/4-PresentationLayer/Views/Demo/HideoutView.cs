using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;
using UnityEngine;
using UnityEngine.Assertions;

namespace Mole.Halt.PresentationLayer
{
    public class HideoutView : ViewNode
    {
        [SerializeField] Collider area;

        private void OnValidate()
        {
            Assert.IsNotNull(area);
        }

        public bool IsPositionInHideout(Position position)
        {
            return area.bounds.Contains(position.ToVector3);
        }
    }
}