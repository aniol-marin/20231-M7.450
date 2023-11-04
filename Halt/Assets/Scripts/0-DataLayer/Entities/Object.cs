namespace Mole.Halt.DataLayer
{
    public class Object : Entity
    {
        public readonly ObjectType Type;
        public readonly string Label;

        public Object(ObjectType type, string label = null) : base(EntityType.Object)
        {
            Type = type;
            Label = label == null ? $"[{EntityType}][{Type}])" : label;
        }

        public Object(EntityId id, ObjectType type, string label = null) : base(EntityType.Object, id)
        {
            Type = type;
            Label = label == null ? $"[{EntityType}][{Type}])" : label;
        }

        public override string ToString()
        {
            return $"{Label} [{Type}][{Id}]";
        }
    }
}