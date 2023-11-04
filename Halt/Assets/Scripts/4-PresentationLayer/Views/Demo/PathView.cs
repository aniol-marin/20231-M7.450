using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mole.Halt.PresentationLayer
{
    public class PathView : ViewNode
    {
        [SerializeField] private List<PathTileView> tiles;
        [SerializeField] private PathTileView origin;
        [SerializeField] private PathTileView end;

        public IEnumerable<Position> InterestPoints => tiles.Select(t => t.Origin);
        public Position Origin => (origin != null) ? origin.Origin : tiles.First().Origin;

        public void FetchTiles()
        {
            tiles ??= new();
            tiles.Clear();

            tiles = gameObject
                .GetComponentsInChildren<PathTileView>(includeInactive: true)
                .ToList();
        }
    }
}