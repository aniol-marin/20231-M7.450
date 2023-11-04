using Mole.Halt.DataLayer;
using UnityEngine;

namespace Mole.Halt.DataAccessLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/Entity Filters/Character")]
    public class CharacterFilter : EntityFilter
    {
        [SerializeField] private CharacterType characterType;

        public override EntityType Entity => EntityType.Character;

        public override Entity Clone(EntityId id)
        {
            return new Character(id, characterType);
        }
    }
}
