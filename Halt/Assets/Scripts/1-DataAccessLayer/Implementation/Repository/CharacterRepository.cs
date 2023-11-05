using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mole.Halt.DataAccessLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/Character Repository")]
    public class CharacterRepository : DataAsset, ICharacterRepository
    {
        private readonly HashSet<Character> activeCharacters = new();

        public event Callback OnRepositoryUpdated = delegate { };

        public void Add(Character character)
        {
            activeCharacters.Add(character);
            OnRepositoryUpdated();
        }

        public void Remove(Character character)
        {
            activeCharacters.Remove(character);
            OnRepositoryUpdated();
        }

        public Character Get(EntityId id)
        {
            return activeCharacters.Single(c => c.Id == id);
        }

        public IEnumerable<Character> GetAll => activeCharacters;

        public IEnumerable<Character> GetAllOfType(CharacterType type)
        {
            return activeCharacters
                .Where(c => c.CharacterType == type);
        }
    }
}