using Mole.Halt.ApplicationLayer;
using Mole.Halt.PresentationLayer.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using PrefabProvider = Mole.Halt.ApplicationLayer.PrefabProvider;

namespace Mole.Halt.PresentationLayer.Views
{
    public class MainMenuScreenView : MonoBehaviour
    {
        [Inject] private readonly DiContainer diContainer;
        [Inject] private readonly PrefabProvider prefabProvider;
        [SerializeField] private Transform container;
        [SerializeField] private List<MenuOptionView> tiles;
        private TileSpawnHelper<MenuOptionView, MenuOption> tileSpawner;
        private Action<MenuOption> onButtonClick;

        public void Init(IEnumerable<MenuOption> options, Action<MenuOption> onButtonClick)
        {
            this.onButtonClick = onButtonClick;

            tileSpawner = new(prefabProvider.Get<MenuOptionView>(), container, tiles, diContainer, HandleTileUpdate, HandleTileToggling);
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
