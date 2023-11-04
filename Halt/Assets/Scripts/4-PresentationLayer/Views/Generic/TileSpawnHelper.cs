using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mole.Halt.PresentationLayer
{
    public class TileSpawnHelper<T, U>
        where T : ViewNode
        where U : struct
    {
        private readonly IPrefabFactory factory;
        private readonly Transform parent;
        private readonly IList<T> container;
        private readonly Callback<T> initTile;
        private readonly Callback<T, U> updateTile;
        private readonly Callback<T, bool> onTileToggle;

        public TileSpawnHelper(
            IPrefabFactory factory,
            Transform parent,
            IList<T> container,
            Callback<T, U> updateTile,
            Callback<T, bool> onTileToggle,
            Callback<T> initTile = null)
        {
            this.factory = factory;
            this.parent = parent;
            this.container = container;
            this.initTile = initTile;
            this.updateTile = updateTile;
            this.onTileToggle = onTileToggle;
        }

        public void PopulateTiles(in IEnumerable<U> data)
        {
            HandleRequestSize(data);

            int provIndex = 0;
            data
                .Select(d => new StructToClass<U>(d))
                .ForEach(e =>
                {
                    updateTile(container.ElementAt(provIndex), (U)e.Content);
                    ++provIndex;
                });
            //.ForEach((e, i) => updateTile(container.ElementAt(i), (U)e.Content));
        }

        private void HandleRequestSize(in IEnumerable<U> data)
        {
            uint requested = (uint)(data?.Count() ?? 0);
            uint avaliable = (uint)container.Count;

            if (requested > avaliable)
            {
                AddTiles(requested - avaliable);
            }
            else
            {
                HideTilesAfterIndex(requested);
            }

            ShowTilesUntilIndex(requested);
        }

        private void AddTiles(uint amount)
        {
            if (amount > 0)
            {
                T[] tiles = new T[amount];

                for (uint i = 0; i < amount; i++)
                {
                    tiles[i] = factory.Instantiate<T>(parent);
                    container?.Add(tiles[i]);
                    initTile?.Invoke(tiles[i]);
                }
            }
        }

        private void ShowTilesUntilIndex(uint index)
        {
            container
                .Take((int)index)
                .ForEach(t => onTileToggle(t, true));
        }

        private void HideTilesAfterIndex(uint index)
        {
            container
                .Skip((int)index)
                .ForEach(t => onTileToggle(t, false));
        }
    }
}