using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using UnityEngine;

namespace Mole.Halt.DataAccessLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/Entity Filters/Character")]
    public class CharacterFilter : EntityFilter
    {
        [SerializeField] private CharacterType characterType;
        [SerializeField] private float baseSpeed;
        [SerializeField] private float speedVariability;

        public override EntityType Entity => EntityType.Character;
        public float Speed => baseSpeed + RandomValue.Float(-speedVariability, speedVariability);

        public override Entity Clone(EntityId id)
        {
            return new Character(id, characterType);
        }
    }
}
