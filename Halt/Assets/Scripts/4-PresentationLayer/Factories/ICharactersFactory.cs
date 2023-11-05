using Mole.Halt.ApplicationLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using UnityEngine;

namespace Mole.Halt.PresentationLayer
{
    public interface ICharactersFactory
    {
        CharacterView Instantiate(Character character, InitialCharacterData data, Transform parent);
    }
    public readonly struct GeneratedCharacter
    {
        public readonly Character character;
        public readonly CharacterView view;
        public readonly ControllerType controller;

        public GeneratedCharacter(Character character, CharacterView view, ControllerType controller)
        {
            this.character = character;
            this.view = view;
            this.controller = controller;
        }
    }
}