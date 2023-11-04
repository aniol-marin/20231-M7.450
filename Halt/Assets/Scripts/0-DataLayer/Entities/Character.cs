namespace Mole.Halt.DataLayer
{
    public sealed class Character : Entity
    {
        public readonly CharacterType CharacterType;
        public MoodType Mood;
        public readonly string Label;

        public Character(CharacterType type, MoodType mood = MoodType.Neutral, string label = null) : base(EntityType.Character)
        {
            CharacterType = type;
            Mood = mood;
            Label = label == null ? $"[{EntityType}][{CharacterType}])" : label;
        }


        /// <summary>
        /// For restoring previous games only
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="id"></param>
        public Character(EntityId id, CharacterType type, MoodType mood = MoodType.Neutral, string label = null) : base(EntityType.Character, id)
        {
            CharacterType = type;
            Mood = mood;
            Label = label == null ? $"[{EntityType}][{CharacterType}])" : label;
        }

        public override string ToString()
        {
            return $"{Label} [{Id}]({Mood})";
        }
    }
}