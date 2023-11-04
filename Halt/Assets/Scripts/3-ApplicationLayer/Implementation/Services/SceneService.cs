using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace Mole.Halt.ApplicationLayer.Internal
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
        private static readonly HashSet<Initializable> initializables = new();

        [Injected] private readonly InitializablesProvider initializablesProvider;
        [Injected] private readonly Scenes scenes;
        private readonly List<Resettable> instances = new();

        public event Callback OnApplicationQuit = delegate { };
        public event Callback OnSceneInitialized = delegate { };
        public event Callback OnSceneEnded = delegate { };
        public event Callback OnSceneChange = delegate { };

        #region Scene Initialization
        public void InitializeScene(NodeClass root)
        {
            initializablesProvider.FetchNodeTree(root)
                .Concat(initializablesProvider.FetchCompositionTree())
                .ForEach(TryInitialize);

            OnSceneInitialized();
        }
        public void TryInitialize(Initializable instance)
        {
            if (initializables.Add(instance))
            {
                instance.Init();
            }
        }

        public void FinalizeScene()
        {
            initializables.ForEach(i => i.Deinit());
            initializables.Clear();

            OnSceneEnded();
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

        #region Scene Switch

        public void ChangeScene(SceneId scene)
        {
            SceneManager.LoadScene(scenes.GetName(scene));
            OnSceneChange();
        }
        #endregion

        #region Application
        /// <summary>
        /// Basic application end method. PC only.
        /// 
        /// WARNING: uses precompiler options in order to manage editor runtime.
        /// </summary>
        public void Quit()
        {
            OnApplicationQuit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            //Application.Quit();
#endif
        }

        #endregion
    }
}