using Mole.Halt.ApplicationLayer;
using Mole.Halt.PresentationLayer.Models;
using Mole.Halt.PresentationLayer.Views;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Mole.Halt.PresentationLayer.Controllers
{
    public class MainMenuScreen : MenuScreen
    {
        [Inject] private readonly ApplicationFinalizer appFinalizer;
        [Inject] private readonly SceneChanger sceneChanger;
        [SerializeField] private MainMenuScreenView view;
        [SerializeField] private OptionData options;

        public override ScreenId ScreenType => ScreenId.Main;

        public override void Init()
        {
            view.Init(options.GetScreenOptions(), HandleButtonClick);
        }

        public override void Deinit()
        {
            Debug.LogWarning("main menu screen finalized");
        }

        private void HandleButtonClick(MenuOption option)
        {
            switch (option)
            {
                case MenuOption.Play:
                    sceneChanger.ChangeScene(SceneId.Demo);
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