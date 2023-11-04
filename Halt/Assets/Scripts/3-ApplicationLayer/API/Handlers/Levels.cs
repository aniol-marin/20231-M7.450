using Mole.Halt.ApplicationLayer.Internal;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mole.Halt.ApplicationLayer
{
    [CreateAssetMenu(fileName = "Levels", menuName = "Mole/Built-in Levels")]

    public class Levels : DataAsset
    {
        [SerializeField] private InitialLevelData levelDesign;
        [SerializeField] private List<InitialLevelData> builtInLevels;
        private LevelFilter selectedLevel;

        public LevelFilter SelectedLevel
        {
            get => selectedLevel;
            set => selectedLevel = value;
        }

        public IEnumerable<LevelFilter> GetAllLevelsInfo()
        {
            return builtInLevels
                .Select(level => level.Filter)
                .Concat(RetrieveInfoFromUpgrades())
                .Concat(RetrieveInfoFromAssetBundle());
        }

        public void SelectLevel(LevelFilter filter)
        {
            SelectedLevel = filter;
        }

        public bool TryGetNextLevel(LevelFilter filter, out InitialLevelData level)
        {
            level = builtInLevels
                .Where(l => l.Filter.category == filter.category)
                .FirstOrDefault(l => l.Filter.number > filter.number);

            return level != null;
        }

        public bool TryGetPreviousLevel(LevelFilter filter, out InitialLevelData level)
        {
            level = builtInLevels
                .Where(l => l.Filter.category == filter.category)
                .LastOrDefault(l => l.Filter.number < filter.number);

            return level != null;
        }

        public bool LevelExists(LevelFilter filter)
        {
            return builtInLevels
                .Any(l => l.Filter == filter);
        }

        public bool NextLevelExists()
        {
            return TryGetNextLevel(selectedLevel, out InitialLevelData _);
        }

        public bool PreviousLevelExists()
        {
            return TryGetPreviousLevel(selectedLevel, out InitialLevelData _);
        }

        public InitialLevelData GetLevel(LevelFilter filter)
        {
            return source(filter.source)
                .Where(l => l.Filter == filter)
                .FirstOrDefault()
                ?? levelDesign;

            IEnumerable<InitialLevelData> source(LevelSource source) => source switch
            {
                LevelSource.builtIn => builtInLevels,
                LevelSource.dlc => RetrieveFromAssetBundle(),
                LevelSource.upgrade => RetrieveFromUpgrades(),
                LevelSource.levelDesign => RetrieveFromLevelDesign(),
                _ => RetrieveFromLevelDesign(),
            };
        }

        private IEnumerable<LevelFilter> RetrieveInfoFromAssetBundle()
        {
            return RetrieveFromAssetBundle()
                .Select(l => l.Filter);
        }

        private IEnumerable<LevelFilter> RetrieveInfoFromUpgrades()
        {
            return RetrieveFromUpgrades()
                .Select(l => l.Filter);
        }

        private IEnumerable<InitialLevelData> RetrieveFromAssetBundle()
        {
            return Enumerable.Empty<InitialLevelData>();
        }

        private IEnumerable<InitialLevelData> RetrieveFromUpgrades()
        {
            return Enumerable.Empty<InitialLevelData>();
        }

        private IEnumerable<InitialLevelData> RetrieveFromLevelDesign()
        {
            return new[] { levelDesign };
        }

#if UNITY_EDITOR
        public void AddOrReplaceLevel(InitialLevelData level)
        {
            if (level.Filter.source == LevelSource.builtIn)
            {
                InitialLevelData presentLevel = builtInLevels
                    .SingleOrDefault(l => l.Filter == level.Filter);

                if (presentLevel != null)
                {
                    int index = builtInLevels.IndexOf(presentLevel);
                    builtInLevels[index] = level;
                    new Info("Updating the baked level into config");
                }
                else
                {
                    builtInLevels.Add(level);
                    builtInLevels.Sort(comparer);
                    new Info("Adding the baked level into config");
                }

                builtInLevels.Sort();

                int comparer(InitialLevelData a, InitialLevelData b)
                {
                    return (int)((a.Filter.category != b.Filter.category)
                        ? a.Filter.category - b.Filter.category
                        : a.Filter.number - b.Filter.number);
                }
            }
            else
            {
                new Warning("The baked level is not build in, it will not be included in the game by default");
            }
        }
#endif
    }
}