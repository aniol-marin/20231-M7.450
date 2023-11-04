using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using UnityEngine;


namespace Mole.Halt.PresentationLayer
{
    public class MenuScreenController : ControllerNode
    {
        [Injected] private readonly IPrefabFactory factory;
        [Injected] private readonly ScreensData screenProvider;
        [Injected] private readonly Navigation navigation;
        [SerializeField] private Transform screenContainer;
        private readonly Dictionary<ScreenId, MenuScreen> screens = new();
        private MenuScreen currentScreen;


        public override void Init()
        {
            base.Init();

            navigation.OnLeavingCurrentScreen += LeaveCurrentScreen;
            navigation.OnEnteringScreen += NavigateToScreen;
        }

        public override void Deinit()
        {
            navigation.OnEnteringScreen -= NavigateToScreen;
            navigation.OnLeavingCurrentScreen -= LeaveCurrentScreen;
        }

        private void NavigateToScreen(ScreenId screen)
        {
            if (!screens.TryGetValue(screen, out currentScreen))
            {
                currentScreen = factory.Instantiate(screenProvider.GetScreenPrefab(screen), screenContainer, prefabArgumentIntended: true);
                screens.Add(screen, currentScreen);
            }

            currentScreen.Enter();
        }

        private void LeaveCurrentScreen()
        {
            currentScreen.Leave();
        }
    }
}