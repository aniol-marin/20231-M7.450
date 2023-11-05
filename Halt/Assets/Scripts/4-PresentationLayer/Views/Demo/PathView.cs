using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = Mole.Halt.DataLayer.Object;

namespace Mole.Halt.PresentationLayer
{
    public class PathView : ObjectView
    {
        [SerializeField] private List<PathTileView> tiles;
        [SerializeField] private PathTileView origin;
        [SerializeField] private PathTileView end;
        private Path cachedEntity;

        public IEnumerable<Position> InterestPoints => tiles.Select(t => t.Origin);
        public Position Origin => (origin != null) ? origin.Origin : tiles.First().Origin;
        protected override Object Entity => cachedEntity ??= new Path(InterestPoints);

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