using Mole.Halt.Utils;
using System;
using UnityEngine;


namespace Mole.Halt.PresentationLayer
{
    public class SettingsScreen : MenuScreen
    {
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
                case MenuOption.Back:
                    navigation.NavigateTo(ScreenId.Main);
                    break;
                default:
                    throw new NotImplementedException($"Option {option} not handled");
            }
        }
    }
}