using Mole.Halt.DataLayer;
using UnityEngine;

namespace Mole.Halt.Design
{
    [CreateAssetMenu(menuName = "Mole/Design/Entities/Character")]
    public class SerializedCharacter : GenericEntityWrapper<Character>
    {
        [SerializeField] private CharacterType CharacterType;
        [SerializeField] private MoodType Mood;
        [SerializeField] private string Label;
        [SerializeField] private EntityId id;

        public override Entity Entity => entity ?? new Character(id, CharacterType, Mood, Label);

        protected override void PopulateInternal()
        {
            this.CharacterType = entity.CharacterType;
            this.Mood = entity.Mood;
            this.Label = entity.Label;
            this.id = entity.Id;
        }
    }
}