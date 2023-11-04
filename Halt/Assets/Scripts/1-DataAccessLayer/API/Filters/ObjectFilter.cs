using Mole.Halt.DataLayer;
using UnityEngine;
using Object = Mole.Halt.DataLayer.Object;

namespace Mole.Halt.DataAccessLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/Entity Filters/Objects")]
    public class ObjectFilter : EntityFilter
    {
        [SerializeField] private ObjectType objectType;

        public override EntityType Entity => EntityType.Object;
        public ObjectType ObjectType => objectType;

        public override Entity Clone(EntityId id)
        {
            return new Object(id, objectType);
        }
    }
}
