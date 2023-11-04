using Mole.Halt.ApplicationLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Mole.Halt.DataAccessLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/CharacterPackage")]
    public class CensusPackage : DataPackage, ICensusPackage
    {
        [Monitored] private List<EntityId> active = new();

        public IEnumerable<EntityId> Active => active;

        public void Clear()
        {
            active.Clear();
        }

        public void AddEntity(EntityId entity)
        {
            active.Add(entity);
        }

        public void RemoveEntity(EntityId entity)
        {
            active.Remove(entity);
        }
    }
}