using Mole.Halt.ApplicationLayer;
using Mole.Halt.DataAccessLayer;
using Mole.Halt.PresentationLayer;
using Mole.Halt.Utils;
using UnityEngine;


namespace Mole.Halt.Design
{
    [LevelDesign]
    public class CharacterPrototype : NodeClass
    {
        [Injected] ILevelLoader levelLoader;
        [Injected] Levels levels;
        [SerializeField] private PremadeCharacter prototype;

        public PremadeCharacter Prototype => prototype;

        private void Awake() // Design time only
        {
            if (levels.SelectedLevel != default)
            {
                new Warning($"mocking and disabling hardcoded character: {transform.parent}/{transform}");
            }

            InitialCharacterData data = new()
            {
                Prototype = prototype,
                InitialPosition = transform.position,
                InitialRotation = transform.rotation,
            };

            levelLoader.RegisterLevelCharacters(new[] { data }, transform.parent);

            Destroy(gameObject);
        }
    }
}