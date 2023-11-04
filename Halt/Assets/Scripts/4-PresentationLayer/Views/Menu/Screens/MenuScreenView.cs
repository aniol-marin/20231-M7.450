using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using UnityEngine;


namespace Mole.Halt.PresentationLayer
{
    public class MenuScreenView : ViewNode
    {
        [Injected] private readonly IPrefabFactory factory;
        [SerializeField] private Transform container;
        [SerializeField] private List<MenuOptionView> tiles;
        private TileSpawnHelper<MenuOptionView, MenuOption> tileSpawner;
        private Callback<MenuOption> onButtonClick;

        public void Init(IEnumerable<MenuOption> options, Callback<MenuOption> onButtonClick)
        {
            this.onButtonClick = onButtonClick;

            tileSpawner = new(factory, container, tiles, HandleTileUpdate, HandleTileToggling);
            tileSpawner.PopulateTiles(options);
        }

        private void HandleTileUpdate(MenuOptionView tile, MenuOption data)
        {
            tile.Init(() => onButtonClick.Invoke(data));
            tile.ChangeLabel(data.ToString());
        }

        private void HandleTileToggling(MenuOptionView tile, bool value)
        {
            tile.ToggleVisibility(value);
        }
    }
}
