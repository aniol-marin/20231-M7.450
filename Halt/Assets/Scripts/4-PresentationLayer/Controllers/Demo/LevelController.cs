using Mole.Halt.ApplicationLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Mole.Halt.PresentationLayer
{
    public class LevelController : ControllerNode
    {
        [Injected] private readonly ILevelLoader levelLoader;
        private readonly List<CharacterView> activeCharacters = new();
        private readonly List<CharacterView> pooledCharacters = new();
        private Level currentLevel;

        public override void Init()
        {
            base.Init();

            levelLoader.OnLevelLoaded += HandleLevelLoaded;
            levelLoader.OnFinalizingCurrentLevel += UnloadCurrentLevel;
            levelLoader.OnCharactersRegistered += HandleCharactersRegistered;
        }

        public override void Deinit()
        {
            levelLoader.OnLevelLoaded -= HandleLevelLoaded;
            levelLoader.OnFinalizingCurrentLevel -= UnloadCurrentLevel;
        }

        private void HandleCharactersRegistered(IEnumerable<GeneratedCharacter> characters)
        {
            AddCharacters(characters.Select(g => g.view));
        }
        public void AddCharacters(IEnumerable<CharacterView> characters)
        {
            activeCharacters.AddRange(characters);
        }

        private void HandleLevelLoaded(Level level)
        {
            currentLevel = level;
        }

        private void UnloadCurrentLevel()
        {
            if (currentLevel != null)
            {
                currentLevel.Deinit();
                Destroy(currentLevel);
            }
        }
    }
}