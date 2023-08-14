using Mole.Halt.PresentationLayer.Views;
using System;
using UnityEngine;

namespace Mole.Halt.ApplicationLayer
{
    [CreateAssetMenu(menuName = "Mole/Prefabs Model")]

    public class PrefabProvider : ScriptableObject
    {
        [SerializeField] MenuOptionView menuOptionView;

        public T Get<T>() where T : MonoBehaviour
        {
            Type type = typeof(T);

            return type.Name switch
            {
                nameof(MenuOptionView) => menuOptionView as T,
                _ => throw new Exception($"Trying to get a non-serialized type:{nameof(T)}")
            };
        }
    }
}