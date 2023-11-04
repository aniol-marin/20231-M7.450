using Mole.Halt.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Mole.Halt.Editor
{
    public class Reserializer : MonoBehaviour
    {
        private const string PREFABS_LOCATION = "Assets";
        private const string ASSETS_LOCATION = "Assets";
        private const string MATERIALS_LOCATION = "Assets";
        private const string ANIMATIONS_LOCATION = "Assets";
        private const string PREFAB_SEARCH = "t:prefab";
        private const string MATERIAL_SEARCH = "*.mat";
        private const string ANIMATION_SEARCH = "*.anim";
        private const string ASSET_SEARCH = "*.asset";
        private const string SCENE_SEARCH = "*.unity";
        private static readonly string ROOT_LOCATION = Application.dataPath.Replace("Assets", "");

        public static void Reserialize()
        {
            int nulls = 0;

            AssetDatabase
                .FindAssets(PREFAB_SEARCH, new string[] { PREFABS_LOCATION })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<GameObject>)
                .ForEach(EditorUtility.SetDirty);

            // Find and set dirty all assets in relevant folder
            DirectoryInfo assetsFolder = new(ASSETS_LOCATION);
            DirectoryInfo materialsFolder = new(MATERIALS_LOCATION);
            DirectoryInfo animationsFolder = new(ANIMATIONS_LOCATION);
            List<FileInfo> assets = new();

            assets.AddRange(assetsFolder.GetFiles(ASSET_SEARCH, SearchOption.AllDirectories));
            assets.AddRange(materialsFolder.GetFiles(MATERIAL_SEARCH, SearchOption.AllDirectories));
            assets.AddRange(animationsFolder.GetFiles(ANIMATION_SEARCH, SearchOption.AllDirectories));
            assets.AddRange(animationsFolder.GetFiles(SCENE_SEARCH, SearchOption.AllDirectories));

            foreach (FileInfo asset in assets)
            {
                string path = Regex.Split(asset.FullName.Replace("\\", "/"), ROOT_LOCATION).Single(s => s.Contains(asset.Name));
                Object item = AssetDatabase.LoadAssetAtPath<Object>(path);
                if (item != null) EditorUtility.SetDirty(item);
                else nulls++;
            }


            IEnumerable<string> pathsDirectory = Directory
                .GetFileSystemEntries($"{Application.dataPath}", searchPattern: "*.*", searchOption: SearchOption.AllDirectories)
                .Select(s => Regex.Split(s, "\\\\").Last())
                .SelectMany(AssetDatabase.FindAssets)
                .Select(AssetDatabase.GUIDToAssetPath);

            pathsDirectory
                .Select(AssetDatabase.LoadMainAssetAtPath);

            AssetDatabase.ForceReserializeAssets(pathsDirectory, ForceReserializeAssetsOptions.ReserializeAssetsAndMetadata);

            AssetDatabase.SaveAssets();

            new Info($"Reserialized the whole project. Rememeber to save or close the editor in order to save the changes");
        }
    }

}