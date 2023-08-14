using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Mole.Halt.ApplicationLayer
{
    public class DemoSceneController : MonoBehaviour
    {
        [Inject] private readonly SceneInitializer initializer;
        [Inject] private readonly SceneFinalizer finalizer;
        [Inject] private readonly ApplicationFinalizer applicationFinalizer;
        [Inject] private readonly SceneChanger sceneChanger;
        [SerializeField] private Button quit;
        [SerializeField] private Button menu;

        private void Awake()
        {
            initializer.OnSceneInitialized += InitHUD;

            initializer.InitializeScene();
        }

        private void OnDestroy()
        {
            finalizer.FinalizeScene();
        }

        private void InitHUD()
        {
            quit.onClick.AddListener(() => applicationFinalizer.Quit());
            menu.onClick.AddListener(() => sceneChanger.ChangeScene(SceneId.Menu));
        }
    }
}