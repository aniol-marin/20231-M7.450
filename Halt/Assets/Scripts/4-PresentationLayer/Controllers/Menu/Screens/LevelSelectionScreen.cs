using Mole.Halt.ApplicationLayer;
using Mole.Halt.ApplicationLayer.Internal;
using Mole.Halt.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Mole.Halt.PresentationLayer
{
    public class LevelSelectionScreen : MenuScreen
    {
        [Injected] private readonly Navigation navigation;
        [Injected] private readonly Levels levels;
        [Injected] private readonly IPrefabFactory prefabFactory;
        [Injected] private readonly SceneChanger scene;
        [SerializeField] private MenuScreenView view;
        [SerializeField] private OptionData options;
        [SerializeField] private Transform buttonsContainer;
        [SerializeField] private List<LevelOptionView> buttons;
        private List<LevelFilter> availableLevels = new();

        public override ScreenId ScreenType => ScreenId.Main;

        public override void Init()
        {
            base.Init();

            view.Init(options.GetScreenOptions(), HandleButtonTriggered);

            availableLevels.Clear();
            availableLevels.AddRange(levels.GetAllLevelsInfo());
            availableLevels
                .ForEach(l =>
            {
                LevelOptionView level = prefabFactory.Instantiate<LevelOptionView>(buttonsContainer);
                level.Init(l, HandleLevelSelected);
                level.ChangeLabel($"{l.category} {l.number}");
                buttons.Add(level);
            });
        }

        public override void Deinit()
        {

        }

        private void HandleButtonTriggered(MenuOption option)
        {
            switch (option)
            {
                case MenuOption.Back:
                    navigation.NavigateTo(ScreenId.Main);
                    break;
                default:
                    throw new NotImplementedException($"Option {option} not handled");
            }
        }

        private void HandleLevelSelected(LevelFilter filter)
        {
            levels.SelectLevel(filter);
            scene.ChangeScene(SceneId.Demo);
        }
    }
}