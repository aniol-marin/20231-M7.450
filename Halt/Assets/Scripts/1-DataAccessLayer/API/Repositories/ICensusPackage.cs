using Mole.Halt.DataLayer;
using System.Collections.Generic;

namespace Mole.Halt.DataAccessLayer
{
    public interface ICensusPackage
    {
        IEnumerable<EntityId> Active { get; }

        void AddEntity(EntityId entity);
        void RemoveEntity(EntityId entity);
        void Clear();
    }
}