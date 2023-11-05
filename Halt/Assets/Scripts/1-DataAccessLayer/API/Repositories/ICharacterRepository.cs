using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;

namespace Mole.Halt.DataAccessLayer
{
    public interface ICharacterRepository
    {
        IEnumerable<Character> GetAll { get; }

        event Callback OnRepositoryUpdated;

        void Add(Character character);
        Character Get(EntityId id);
        IEnumerable<Character> GetAllOfType(CharacterType type);
        void Remove(Character character);
    }
}