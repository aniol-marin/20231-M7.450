using Mole.Halt.ApplicationLayer.Internal;
using Mole.Halt.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mole.Halt.ApplicationLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/Level")]
    public class InitialLevelData : DataAsset
#if UNITY_EDITOR
        , IComparable<InitialLevelData>
#endif
    {
        [Header("Classification")]
        [SerializeField] private LevelNumber number;
        [SerializeField] private LevelCategory category;
        [SerializeField] private LevelSource source;
        [Header("Content")]
        [SerializeField] private Level level;
        [SerializeField] private List<InitialCharacterData> characters;

        public LevelFilter Filter => new(number, category, source);
        public Level Level => level;
        public IEnumerable<InitialCharacterData> InitialCharacterData => characters;

#if UNITY_EDITOR
        public void ManuallySetData(Level level, IEnumerable<InitialCharacterData> characters)
        {
            this.level = level;

            this.characters = new();
            this.characters.AddRange(characters);
        }

        public int CompareTo(InitialLevelData other)
        {
            return
                this.Filter.source != other.Filter.source
                ? this.Filter.source.CompareTo(other.Filter.source)
                : this.Filter.category != other.Filter.category
                    ? this.Filter.category.CompareTo(other.Filter.category)
                    : this.Filter.number.CompareTo(other.Filter.number);
        }
#endif
    }
}