using Mole.Halt.DataLayer;
using Mole.Halt.PresentationLayer;
using Mole.Halt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mole.Halt.Meta
{
    [CreateAssetMenu(menuName = "Mole/Halt/Assets/Character Prefabs")]
    public class CharacterPrefabLoader : DataAsset
    {
        [SerializeField] private CharacterPair defaultView;
        [SerializeField] private List<CharacterPair> pairs;

        public CharacterView GetPrefab(CharacterType type)
        {
            return pairs
                .FirstOrDefault(p => p.type == type, defaultView)
                .view;
        }

        [Serializable]
        private class CharacterPair
        {
            public CharacterType type;
            public CharacterView view;
        }
    }
}