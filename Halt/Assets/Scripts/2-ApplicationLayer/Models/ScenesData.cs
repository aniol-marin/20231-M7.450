using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Mole.Halt.ApplicationLayer
{
    [CreateAssetMenu(menuName = "Mole/Scene Model")]

    public class ScenesData : ScriptableObject
    {
        [SerializeField] private List<ScenePair> scenes;

        public string GetSceneName(SceneId id)
        {
            return scenes
                .First(scenes => scenes.key == id)
                .scene
                .name;
        }
    }

    [Serializable]
    public struct ScenePair
    {
        public SceneId key;
        public Object scene;
    }

    public enum SceneId
    {
        None = 0,
        Menu = 1,
        Demo = 2,
    }
}