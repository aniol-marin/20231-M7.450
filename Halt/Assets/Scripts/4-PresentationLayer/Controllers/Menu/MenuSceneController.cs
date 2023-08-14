using Mole.Halt.PresentationLayer;
using UnityEngine;
using Zenject;

namespace Mole.Halt.ApplicationLayer
{
    public class MenuSceneController : MonoBehaviour
    {
        [Inject] private readonly DiContainer diContainer;
        [Inject] private readonly SceneInitializer initializer;
        [Inject] private readonly SceneFinalizer finalizer;
        [Inject] private readonly ScreensData screenProvider;
        [SerializeField] private Transform screenContainer;

        private void Awake()
        {
            initializer.OnSceneInitialized += MockFirstScreen;

            initializer.InitializeScene();
        }

        private void OnDestroy()
        {
            finalizer.FinalizeScene();
        }

        private void MockFirstScreen()
        {
            MenuScreen firstScreen = diContainer.InstantiatePrefabForComponent<MenuScreen>(screenProvider.GetScreenPrefab(PresentationLayer.Models.ScreenId.Main), screenContainer);
            firstScreen.Init();
            firstScreen.Enter();
        }
    }
}