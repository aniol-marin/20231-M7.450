using Mole.Halt.ApplicationLayer;
using Mole.Halt.Design;
using Mole.Halt.PresentationLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using Unity.AI.Navigation.Editor;
using UnityEditor;
using UnityEngine;
using SerializedObject = UnityEditor.SerializedObject;

namespace Mole.Halt.Editor
{
    [CustomEditor(typeof(LevelMaker))]
    public class LevelMakerEditor : UnityEditor.Editor
    {
        private LevelMaker levelMaker;

        private void OnEnable()
        {
            levelMaker = target as LevelMaker;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Label("Asset management tools");
            LoadFromConfigButton();
            BakeLevelButton();
            GUILayout.Label("Level Design tools");
            BakeNavMeshButton();
        }

        private void LoadFromConfigButton()
        {
            if (GUILayout.Button("Load from config"))
            {
                LoadFromConfig();
            }
        }

        private void BakeNavMeshButton()
        {
            if (GUILayout.Button("Bake LevelMaker Navigation"))
            {
                BakeTestMesh();
            }
        }

        private void BakeLevelButton()
        {
            if (GUILayout.Button("Bake Level"))
            {
                BakeLevel();
            }
        }

        private void UnlinkPreviousNavMeshes()
        {
            // clear the navigation links from the MAKER prefab (othwerise it overwrites the ones in created levels)
            levelMaker
                .gameObject
                .GetComponentsInChildren<NavMeshSurface>()
                .ForEach(s => s.navMeshData = null);
        }

        async private void BakeLevel()
        {
            string levelName = $"level-{levelMaker.category}-{(uint)levelMaker.number}";

            // Bake surfaces (async)
            AssetDatabase.SaveAssets();
            await BakeMesh(levelMaker.gameObject, levelName);


            // Instantiating new prefab from template
            GameObject prefab = AssetDatabase
                 .FindAssets($"t:{nameof(GameObject)}")  // narrows down to prefabs
                 .Select(AssetDatabase.GUIDToAssetPath)  // selects path
                 .Where(r => r.Contains(levelMaker.name))     // selects its own asset
                 .Select(PrefabUtility.LoadPrefabContents)
                 .Debug(p => { new Info($"Started to bake level from {p}"); })
                 .Single();

            // unlink navmesh, otherwise overwrites the generated level one in the next batch
            UnlinkPreviousNavMeshes();

            // Instantiate new scriptable (will be used later)
            InitialLevelData data = CreateInstance<InitialLevelData>();

            // Create temporary list
            List<InitialCharacterData> characters = new();

            // Fetch and destroy character prototypes
            prefab
#pragma warning disable CS0618 // intended usage
                .GetComponentsInChildren<CharacterPrototype>(includeInactive: true)
#pragma warning restore CS0618 // !
                .ForEach(c =>
                {
                    characters.Add(new()
                    {
                        Prototype = c.Prototype,
                        InitialPosition = c.transform.position,
                        InitialRotation = c.transform.rotation,
                    });

                    EditorUtility.SetDirty(c);
                    DestroyImmediate(c.gameObject, allowDestroyingAssets: false);
                });

            // Destroy hardcoded characters, if any, with a warning
            prefab
                .GetComponentsInChildren<CharacterView>(includeInactive: true)
                .ForEach(c =>
                {
#pragma warning disable CS0618 // intended usage
                    new Warning($"found a hardcoded character in the level making scene. It will be ignored an removed from the built level. Please use {nameof(CharacterPrototype)} instead of {nameof(CharacterView)} for level design");
#pragma warning restore CS0618 // !
                    EditorUtility.SetDirty(c);
                    DestroyImmediate(c.gameObject, allowDestroyingAssets: false);
                });

            // Fetch development-time LevelMaker, add production-time Level equivalent
            LevelMaker generatedLevelMaker = prefab.GetComponentInChildren<LevelMaker>();
            Level level = prefab.AddComponent<Level>();

            // Copy relevant private serialized fields from LevelMaker to Level
            CopyProperty(generatedLevelMaker, level, "characterContainer");

            // Remove unnecessary LevelMaker
            EditorUtility.SetDirty(generatedLevelMaker);
            DestroyImmediate(generatedLevelMaker);

            // Save prefab
            string prefabFile = $"prefab-{levelName}.prefab";
            string relativePrefabPath = $"Components/Levels/{levelMaker.category}/";
            string absolutePrefabPath = $"{Application.dataPath}/{relativePrefabPath}";
            string relativePrefabFilePath = $"Assets/{relativePrefabPath}{prefabFile}";
            string absolutePrefabFilePath = $"{absolutePrefabPath}{prefabFile}";
            System.IO.Directory.CreateDirectory(absolutePrefabPath);
            GameObject savedPrefab = PrefabUtility.SaveAsPrefabAsset(prefab, absolutePrefabFilePath);
            new Info($"Level baked as {prefabFile} at {relativePrefabPath}");


            // Fill asset
            CopyProperty(levelMaker, data, "number");
            CopyProperty(levelMaker, data, "category");
            CopyProperty(levelMaker, data, "source");
            data.ManuallySetData(savedPrefab.GetComponent<Level>(), characters);

            // Save asset
            string assetFile = $"asset-level-{levelMaker.category}-{(int)levelMaker.number}.asset";
            string relativeAssetPath = $"Configurations/Levels/{levelMaker.category}/";
            string absoluteAssetPath = $"{Application.dataPath}/{relativeAssetPath}";
            string relativeAssetFilePath = $"{relativeAssetPath}{assetFile}";
            string absoluteAssetFilePath = $"{absoluteAssetPath}{assetFile}";
            System.IO.Directory.CreateDirectory(absoluteAssetPath);
            AssetDatabase.CreateAsset(data, $"Assets/{relativeAssetFilePath}");
            new Info($"Level baked as {assetFile} at {relativeAssetFilePath}");
            //Fetch the now saved asset
            InitialLevelData savedData = AssetDatabase
                .FindAssets($"t:{nameof(InitialLevelData)}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Where(a => a.Contains(data.name))
                .Select(AssetDatabase.LoadAssetAtPath<InitialLevelData>)
                .Single();

            // Fetch and update config asset
            Levels levels = AssetDatabase
                .FindAssets($"t:{nameof(Levels)}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Levels>)
                .Single();

            levels.AddOrReplaceLevel(savedData);
        }

        private bool CopyProperty<T, U>(T source, U target, string propertyName) where T : Object where U : Object
        {
            SerializedObject sSource = new(source);
            SerializedObject sTarget = new(target);
            SerializedProperty sProperty = sSource.FindProperty(propertyName);
            try
            {
                //[NOTE] this external method may also WRITE A GENERIC ERROR without throwing exception. Thanks Unity.
                sTarget.CopyFromSerializedProperty(sProperty);
            }
            catch { new Error($"Mapped null property [{propertyName}] from [{typeof(T).Name}] to [{typeof(U).Name}]"); }
            EditorUtility.SetDirty(target);
            return sTarget.ApplyModifiedProperties();
        }

        private void LoadFromConfig()
        {
            new Warning($"<color=red>Loading fom Editor not implemented</color>");
        }

        async private void BakeTestMesh()
        {
            await BakeMesh(levelMaker.gameObject, levelMaker.name);
        }

        private async Task BakeMesh(GameObject target, string levelName)
        {
            string assetName = $"asset-navmesh-{levelName}";

            new Info($"Starting to bake NavMeshSurfaces for {levelName}");

            // cleanup of previous baked attempts
            bool failedCleanups = false;
            AssetDatabase
                .FindAssets(assetName)
                .Select(AssetDatabase.GUIDToAssetPath)
                .ForEach(a => failedCleanups |= !AssetDatabase.DeleteAsset(a));
            if (failedCleanups)
            {
                new Warning($"Failed to delete some old bakes for {levelName}");
            }
            await Task.Yield();

            // bake
            int surfaceCount = 0;
            FetchSurfaces(target)
                .ForEach(async p =>
                {
                    ++surfaceCount;

                    NavMeshAssetManager.instance.StartBakingSurfaces(p.surfaces);

                    await Task.Delay(100 * p.surfaces.Length); // precalculated initial delay
                    while (p.surfaces.Any(NavMeshAssetManager.instance.IsSurfaceBaking))
                    {
                        new Info($"waiting for mesh in {p.name} to finish baking...");
                        await Task.Delay(100);
                    };

                    // Hardcoded delay. Once NavMeshAssetManager finishes, I didn't find a cleaner way
                    // to wait until only the new asset is present (waiting until only one asset is left
                    // bears no effect, it end up renaming the old one)
                    new Info("waiting mesh assets to finish writing...");
                    await Task.Delay(1000);

                    string folder = "Components/NavMeshes";
                    string dataBaseFolder = $"Assets/{folder}";
                    System.IO.Directory.CreateDirectory($"{Application.dataPath}/{folder}");
                    string oldPath = AssetDatabase
                        .FindAssets(p.name)
                        .Select(AssetDatabase.GUIDToAssetPath)
                        .Single();
                    AssetDatabase.MoveAsset(oldPath, $"{dataBaseFolder}/{assetName}-surface{surfaceCount}.asset");
                });

            if (surfaceCount == 0)
            {
                new Warning("trying to bake a level without a valid NavNeshSurface");
            }
            else
            {
                new Info("NavMesh baking done");
            }

            await Task.Yield();
        }

        private IEnumerable<SurfaceNamePair> FetchSurfaces(GameObject target)
        {
            return target
                .GetComponentsInChildren<NavMeshSurface>(includeInactive: true)
                .Select(c =>
                {
                    string holder = c.gameObject.name;
                    return new SurfaceNamePair(holder, new[] { c });
                });
        }

        private readonly struct SurfaceNamePair
        {
            public readonly string name;
            public readonly NavMeshSurface[] surfaces;

            public SurfaceNamePair(string name, NavMeshSurface[] surfaces)
            {
                this.name = name;
                this.surfaces = surfaces;
            }
        }
    }
}