using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using UnityEngine;

namespace Mole.Halt.DataAccessLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/Character prototype")]
    public class PremadeCharacter : DataAsset
    {
        [SerializeField] private string label;
        [SerializeField] private CharacterType character;
        [SerializeField] private MoodType mood;
        [SerializeField] private ControllerType controller;

        public string Label => label;
        public CharacterType Character => character;
        public MoodType Mood => mood;
        public ControllerType ControllerType => controller;
    }
}