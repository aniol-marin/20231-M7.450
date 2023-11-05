using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = Mole.Halt.DataLayer.Object;

namespace Mole.Halt.DataAccessLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/Object Repository")]
    public class ObjectRepository : DataAsset, IObjectRepository
    {
        private readonly HashSet<Object> activeObjects = new();

        public event Callback OnRepositoryUpdated = delegate { };

        public void Add(Object item)
        {
            activeObjects.Add(item);
            OnRepositoryUpdated();
        }

        public void Remove(Object character)
        {
            activeObjects.Remove(character);
            OnRepositoryUpdated();
        }

        public IEnumerable<Object> GetAll => activeObjects;

        public IEnumerable<Object> GetAllOfType(ObjectType type)
        {
            return activeObjects
                .Where(c => c.Type == type);
        }

        public bool TryGet<T>(EntityId id, out T @object) where T : Object
        {
            @object = activeObjects.FirstOrDefault(o => o.Id == id) as T;
            return @object != null;
        }
    }
}