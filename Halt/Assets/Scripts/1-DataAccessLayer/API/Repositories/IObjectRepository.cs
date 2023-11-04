using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;

namespace Mole.Halt.DataAccessLayer
{
    public interface IObjectRepository
    {
        IEnumerable<Object> GetAll { get; }

        event Callback OnRepositoryUpdated;

        void Add(Object item);
        IEnumerable<Object> GetAllOfType(ObjectType type);
        void Remove(Object character);
        bool TryGet<T>(EntityId id, out T @object) where T : Object;
    }
}