using Mole.Halt.Utils;
using System;
using UnityEngine;

namespace Mole.Halt.PresentationLayer.Internal
{
    [CreateAssetMenu(fileName = "Prefabs", menuName = "Mole/Prefabs Model")]

    public class Prefabs : DataAsset
    {
        [SerializeField] private MenuOptionView menuOptionView;
        [SerializeField] private LevelOptionView LevelOptionView;
        [SerializeField] private CharacterView MockCharacter;

        public virtual T Get<T>() where T : NodeClass
        {
            Type type = typeof(T);

            return type.Name switch
            {
                nameof(MenuOptionView) => menuOptionView as T,
                nameof(LevelOptionView) => LevelOptionView as T,
                nameof(CharacterView) => MockCharacter as T,
                _ => throw new Exception($"Trying to get a non-serialized type:{nameof(T)}")
            };
        }
    }
}