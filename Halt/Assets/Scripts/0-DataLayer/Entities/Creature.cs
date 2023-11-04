namespace Mole.Halt.DataLayer
{
    public sealed class Creature : Entity
    {
        public readonly CreatureType CreatureType;

        public Creature(CreatureType type) : base(EntityType.Character)
        {
            CreatureType = type;
        }

        public Creature(EntityId id, CreatureType type) : base(EntityType.Character, id)
        {
            CreatureType = type;
        }

        public override string ToString()
        {
            return $"[{Id}][{EntityType}][{CreatureType}])";
        }
    }
}