using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;
using System;
using UnityEngine;


namespace Mole.Halt.PresentationLayer
{
    public class MainMenuScreen : MenuScreen
    {
        [Injected] private readonly ApplicationFinalizer appFinalizer;
        [Injected] private readonly SceneChanger sceneChanger;
        [Injected] private readonly Navigation navigation;
        [SerializeField] private MenuScreenView view;
        [SerializeField] private OptionData options;

        public override ScreenId ScreenType => ScreenId.Main;

        public override void Init()
        {
            base.Init();
            view.Init(options.GetScreenOptions(), HandleButtonTriggered);
        }

        public override void Deinit()
        {

        }

        private void HandleButtonTriggered(MenuOption option)
        {
            switch (option)
            {
                case MenuOption.Play:
                    sceneChanger.ChangeScene(SceneId.Demo);
                    break;
                case MenuOption.Levels:
                    navigation.NavigateTo(ScreenId.LevelSelection);
                    break;
                case MenuOption.Settings:
                    navigation.NavigateTo(ScreenId.Settings);
                    break;
                case MenuOption.Quit:
                    appFinalizer.Quit();
                    break;
                default:
                    throw new NotImplementedException($"Option {option} not handled");
            }
        }
    }
}