using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;
using UnityEngine;

namespace Mole.Halt.PresentationLayer
{
    public class PathTileView : ViewNode
    {
        [SerializeField] private Transform origin;

        public Position Origin => new(origin.position);
    }
}