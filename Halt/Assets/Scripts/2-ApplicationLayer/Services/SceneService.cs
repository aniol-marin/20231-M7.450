using Mole.Halt.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using Zenject;

namespace Mole.Halt.ApplicationLayer
{
    /// <summary>
    /// Service for runtime scene management
    /// </summary>
    public class SceneService :
        SceneChanger,
        ApplicationFinalizer,
        SceneInitializer,
        SceneFinalizer
    {
        [Inject] private readonly DiContainer diContainer;
        [Inject] private readonly ScenesData scenes;
        private List<Initializable> initializables = new();
        private List<Resettable> instances = new();

        public event Action OnApplicationQuit;
        public event Action OnSceneInitialized;
        public event Action OnSceneEnded;
        public event Action OnSceneChange;

        #region Scene Initialization
        public void InitializeScene()
        {
            FetchInitializables();
            initializables.ForEach(i => i.Init());

            OnSceneInitialized?.Invoke();
        }
        public void FinalizeScene()
        {
            initializables.ForEach(i => i.Deinit());
            initializables.Clear();

            OnSceneEnded?.Invoke();
        }

        #endregion

        #region Scene Reset

        /// <summary>
        /// Implementation of IResetter.
        /// Requests all the Resettable listeners to repatch listeners.
        /// </summary>

        public void AttachListeners()
        {
            foreach (Resettable instance in instances)
            {
                //instance?.AttachListeners();
            }
        }

        /// <summary>
        /// Implementation of IResetter.
        /// Requests all the Resettable listeners to reset.
        /// </summary>
        public void ResetAllData()
        {
            foreach (Resettable instance in instances)
            {
                instance?.ResetData();
            }
        }

        #endregion


        /// <summary>
        /// Loads next level upon completion of the current one.
        /// 
        /// Loads End Screen overlay, times the effective scene changes upon completion.
        /// Handles the end of the game returning to the main menu.
        /// </summary>
        private void LoadNextLevel()
        {
            int scene = SceneManager.GetActiveScene().buildIndex + 1;

            scene = scene < SceneManager.sceneCountInBuildSettings ? scene : 0;
            SceneManager.LoadScene(scene);

            AttachListeners();
            ResetAllData();
        }

        public void ChangeScene(SceneId scene)
        {
            SceneManager.LoadScene(scenes.GetSceneName(scene));
            OnSceneChange?.Invoke();
        }

        /// <summary>
        /// Basic application end method. PC only.
        /// 
        /// WARNING: uses precompiler options in order to manage editor runtime.
        /// </summary>
        public void Quit()
        {
            OnApplicationQuit?.Invoke();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            //Application.Quit();
#endif
        }

        private void FetchInitializables()
        {
            if (initializables.Empty())
            {
                diContainer
                    .AllContracts
                    .Where(i => i.Type.FullName.Contains("Mole")) // narrows down to specific targets (string can still be narrowed down)
                    .Where(c =>
                    {
                        return c.Type // refers to the binded type, not the implementation (which could be repeated by binding multiple interfaces)
                            .GetInterfaces()
                            .Contains(typeof(Initializable));
                    })
                    .ToList()
                    .ForEach(t =>
                    {
                        Initializable i = diContainer.Resolve(t) as Initializable;
                        initializables.Add(i);
                    });
            }
        }
    }
}