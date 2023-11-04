using Mole.Halt.DataLayer;
using UnityEngine;
using Object = Mole.Halt.DataLayer.Object;

namespace Mole.Halt.Design
{
    [CreateAssetMenu(menuName = "Mole/Design/Entities/Character")]
    public class SerializedObject : GenericEntityWrapper<Object>
    {
        [SerializeField] private ObjectType Type;
        [SerializeField] private string Label;
        [SerializeField] private EntityId id;

        public override Entity Entity => entity ?? new Object(id, Type, Label);

        protected override void PopulateInternal()
        {
            this.Type = entity.Type;
            this.Label = entity.Label;
            this.id = entity.Id;
        }
    }
}