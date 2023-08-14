using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mole.Halt.PresentationLayer.Models
{
    [CreateAssetMenu(menuName = "Mole/Menu Option Model")]
    public class OptionData : ScriptableObject
    {
        [SerializeField] private List<MenuOption> options;

        public IEnumerable<MenuOption> GetScreenOptions()
        {
            return options;
        }
    }

    public enum MenuOption
    {
        Play = 1,
        Quit = 2,
    }
}