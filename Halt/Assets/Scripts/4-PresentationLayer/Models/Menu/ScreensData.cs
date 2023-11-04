using Mole.Halt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mole.Halt.PresentationLayer
{
    [CreateAssetMenu(menuName = "Mole/Screen Model")]
    public class ScreensData : DataAsset
    {
        [SerializeField] private List<ScreenPair> screens;

        public MenuScreen GetScreenPrefab(ScreenId id)
        {
            return screens
                .First(scenes => scenes.key == id)
                .screen;
        }
    }

    [Serializable]
    public struct ScreenPair
    {
        public ScreenId key;
        public MenuScreen screen;
    }
}