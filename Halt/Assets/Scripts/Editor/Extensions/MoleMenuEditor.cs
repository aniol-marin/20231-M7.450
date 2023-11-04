using Mole.Halt.ApplicationLayer;
using Mole.Halt.DataAccessLayer;
using Mole.Halt.Design;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Mole.Halt.Editor
{
    public class MoleMenuEditor
    {
        private const string ROOT = "Mole/";
        private const string SCENES = "Scenes/";
        private const string MONITORING = "Monitoring/";
        private const string REPOSITORIES = "Repositories/";
        private const string TASKS = "Tasks/";

        [MenuItem(ROOT + SCENES + "Menu")]
        private static void LoadMenu()
        {
            LoadScene(SceneId.Menu);
        }

        [MenuItem(ROOT + SCENES + "Demo")]
        private static void LoadDemo()
        {
            LoadScene(SceneId.Demo);
        }

        [MenuItem(ROOT + SCENES + "Logic")]
        private static void LoadLogic()
        {
            LoadScene(SceneId.Logic);
        }

        [MenuItem(ROOT + SCENES + "Level Maker")]
        private static void LoadLevelMaker()
        {
            string path = AssetDatabase
                .FindAssets($"t:{nameof(GameObject)}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(p => AssetDatabase.LoadAssetAtPath<GameObject>(p).TryGet(out LevelMaker _))
                .Single();

            LoadScene(SceneId.Demo);
            PrefabStageUtility.OpenPrefab(path);
        }

        [MenuItem(ROOT + TASKS + "Build Asset Bundles")]
        private static void BuildAssetBundles()
        {
            BuildAssetBundles("Builds/DLC");
        }

        [MenuItem(ROOT + TASKS + "Reserialize project")]
        private static void Reserialize()
        {
            Reserializer.Reserialize();
        }

        [MenuItem(ROOT + MONITORING + REPOSITORIES + "Characters")]
        private static void ShowCharacterRepository()
        {
            string assetId = AssetDatabase
                .FindAssets($"t:{typeof(CharacterRepository)}")
                .Single();
            CharacterRepository asset = AssetDatabase
                .LoadAssetAtPath<CharacterRepository>(AssetDatabase.GUIDToAssetPath(assetId));

            Selection.activeObject = asset;
            SceneView.FrameLastActiveSceneView();
        }

        [MenuItem(ROOT + MONITORING + REPOSITORIES + "Objects")]
        private static void ShowObjectRepository()
        {
            string assetId = AssetDatabase
                .FindAssets($"t:{typeof(ObjectRepository)}")
                .Single();
            ObjectRepository asset = AssetDatabase
                .LoadAssetAtPath<ObjectRepository>(AssetDatabase.GUIDToAssetPath(assetId));

            Selection.activeObject = asset;
            SceneView.FrameLastActiveSceneView();
        }

        #region Implementations
        private static Scenes scenes;
        private static void LoadScene(SceneId id)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                if (scenes == null)
                {
                    string assetId = AssetDatabase
                        .FindAssets($"t:{typeof(Scenes)}")
                        .Single();
                    scenes = AssetDatabase.LoadAssetAtPath<Scenes>(AssetDatabase.GUIDToAssetPath(assetId));
                }

                string sceneName = scenes.GetName(id);
                string scenePath = AssetDatabase.GUIDToAssetPath(AssetDatabase
                        .FindAssets($"t:scene {sceneName}")
                        .Single());

                EditorSceneManager.OpenScene(scenePath);
            }
        }

        private static void BuildAssetBundles(string folder)
        {
            /*
            BuildAssetBundleOptions options = BuildAssetBundleOptions.None
                | BuildAssetBundleOptions.AssetBundleStripUnityVersion
                | BuildAssetBundleOptions.ForceRebuildAssetBundle;
             */

            IEnumerable<BuildTarget> targets = new[]
            {
               BuildTarget.StandaloneWindows,
               BuildTarget.StandaloneWindows64,
            };

            targets
                .ToList()
                .ForEach(target =>
                {
                    string subfolder = $"{folder}/{target}";
                    if (!Directory.Exists(subfolder))
                    {
                        Directory.CreateDirectory(subfolder);
                    }
                    new Error("Asset Bundle building currently unavailable");
                    /*
                    BuildPipeline.BuildAssetBundles(subfolder, options, target);
                     */
                });
        }
        #endregion
    }
}