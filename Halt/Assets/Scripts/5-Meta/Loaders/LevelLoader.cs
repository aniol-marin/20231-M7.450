using Mole.Halt.ApplicationLayer;
using Mole.Halt.ApplicationLayer.Internal;
using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.PresentationLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using UnityEngine;


namespace Mole.Halt.Meta
{
    public class LevelLoader : ILevelLoader
    {
        [Injected] private readonly IQueueManager queue;
        [Injected] private readonly IPrefabFactory factory;
        [Injected] private readonly ICharactersFactory charactersFactory;
        [Injected] private readonly Levels levelsProvider;
        [Injected] private readonly EntityMappingService characterMapping;


        //public event Callback OnNewLevelRequested = delegate { };
        public event Callback<Level> OnLevelLoaded = delegate { };
        public event IterableCallback<GeneratedCharacter> OnCharactersRegistered = delegate { };
        public event Callback OnFinalizingCurrentLevel = delegate { };

        public void Load(LevelFilter filter, Transform levelContainer)
        {
            InitialLevelData levelData = levelsProvider.GetLevel(filter);
            Load(levelData, levelContainer);

        }

        public void Load(InitialLevelData data, Transform levelContainer)
        {
            OnFinalizingCurrentLevel();

            levelsProvider.SelectedLevel = data.Filter;
            LoadLevel(data, levelContainer);
            /*
            await LoadLevel(level, levelContainer);

            MockRegisterLevel(null);

            OnLevelRequested(level);
             */
        }

        //private async void LoadLevel(LevelFilter filter, Transform levelContainer)
        private Level LoadLevel(InitialLevelData data, Transform levelContainer)
        {
            Level level = factory.Instantiate(data.Level, levelContainer, prefabArgumentIntended: true);
            OnLevelLoaded(level);
            RegisterLevelCharacters(data.InitialCharacterData, level.CharactersContainer);

            return level;


        }

        public void RegisterLevelCharacters(IEnumerable<InitialCharacterData> characters, Transform container)
        {
            List<GeneratedCharacter> data = new();

            characters
                .ForEach(c =>
                {
                    Character character = new(c.Prototype.Character, c.Prototype.Mood, c.Prototype.Label);
                    CharacterView view = charactersFactory.Instantiate(character, c, container);

                    RegistrationEvent registration = new(character);
                    characterMapping.Add(character, view.Colliders);
                    characterMapping.Add(character, view);
                    queue.ReportEvent(registration);

                    GeneratedCharacter generatedData = new(character, view, c.Prototype.ControllerType);
                    data.Add(generatedData);

                });

            OnCharactersRegistered(data);
        }
    }

}