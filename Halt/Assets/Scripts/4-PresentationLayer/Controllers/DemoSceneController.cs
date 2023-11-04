using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;
using UnityEngine;
using UnityEngine.UI;
using Text = Mole.Halt.ApplicationLayer.Text;

namespace Mole.Halt.PresentationLayer
{
    public class DemoSceneController : ControllerNode
    {
        [Injected] private readonly ApplicationFinalizer applicationFinalizer;
        [Injected] private readonly SceneChanger sceneChanger;
        [Injected] private readonly Timeline timeline;
        [Injected] private readonly ILevelLoader levelLoader;
        [Injected] private readonly Levels levels;
        [SerializeField] private Transform levelContainer;
        [SerializeField] private Text level;
        [SerializeField] private Button quit;
        [SerializeField] private Button menu;
        [SerializeField] private Button next;
        [SerializeField] private Button previous;

        public override void Init()
        {
            base.Init();

            quit.onClick.AddListener(() => applicationFinalizer.Quit());
            menu.onClick.AddListener(() => sceneChanger.ChangeScene(SceneId.Menu));
            next.onClick.AddListener(NextLevel);
            previous.onClick.AddListener(PreviousLevel);
            UpdateVisuals();

            levelLoader.Load(levels.SelectedLevel, levelContainer);
        }

        public override void Deinit()
        {
        }

        private void Start() // Effective match start
        {
            timeline.Play();
        }

        private void NextLevel()
        {
            bool exists = levels.TryGetNextLevel(levels.SelectedLevel, out InitialLevelData level);
            if (exists)
            {
                levelLoader.Load(level, levelContainer);
            }
            UpdateVisuals();
        }
        private void PreviousLevel()
        {
            bool exists = levels.TryGetPreviousLevel(levels.SelectedLevel, out InitialLevelData level);
            if (exists)
            {
                levelLoader.Load(level, levelContainer);
            }
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            previous.interactable = levels.PreviousLevelExists();
            next.interactable = levels.NextLevelExists();
            level.text = $"{levels.SelectedLevel.category} {(int)levels.SelectedLevel.number}";
        }
    }
}