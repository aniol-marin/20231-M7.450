using Mole.Halt.PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mole.Halt.PresentationLayer
{
    [CreateAssetMenu(menuName = "Mole/Screen Model")]
    public class ScreensData : ScriptableObject
    {
        [SerializeField] private List<ScreenPair> scenes;

        public GameObject GetScreenPrefab(ScreenId id)
        {
            return scenes
                .First(scenes => scenes.key == id)
                .screen
                .gameObject;
        }
    }

    [Serializable]
    public struct ScreenPair
    {
        public ScreenId key;
        public MenuScreen screen;
    }
}