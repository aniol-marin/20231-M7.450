using Mole.Halt.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class TileSpawnHelper<T, U>
    where T : MonoBehaviour
    where U : struct
{
    private readonly DiContainer diContainer;
    private readonly GameObject prefab;
    private readonly Transform parent;
    private readonly List<T> container;
    private readonly Action<T> initTile;
    private readonly Action<T, U> updateTile;
    private readonly Action<T, bool> onTileToggle;

    public TileSpawnHelper(
        T prefab, Transform parent, List<T> container,
        DiContainer diContainer,
        Action<T, U> updateTile,
        Action<T, bool> onTileToggle,
        Action<T> initTile = null)
    {
        this.prefab = prefab.gameObject;
        this.parent = parent;
        this.container = container;
        this.diContainer = diContainer;
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
            .ForEach(e  =>
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
                T tile = diContainer.InstantiatePrefabForComponent<T>(prefab, parent);
                tiles[i] = tile;
                initTile?.Invoke(tile);
            }

            container.AddRange(tiles);
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
