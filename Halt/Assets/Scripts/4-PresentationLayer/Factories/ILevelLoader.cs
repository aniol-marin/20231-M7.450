using Mole.Halt.ApplicationLayer;
using Mole.Halt.ApplicationLayer.Internal;
using Mole.Halt.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Mole.Halt.PresentationLayer
{
    public interface ILevelLoader
    {
        event IterableCallback<GeneratedCharacter> OnCharactersRegistered;
        event Callback OnFinalizingCurrentLevel;
        event Callback<Level> OnLevelLoaded;

        void Load(InitialLevelData data, Transform levelContainer);
        void Load(LevelFilter filter, Transform levelContainer);
        void RegisterLevelCharacters(IEnumerable<InitialCharacterData> characters, Transform container);
    }
}