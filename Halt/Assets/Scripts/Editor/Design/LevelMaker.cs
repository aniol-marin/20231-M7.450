using Mole.Halt.ApplicationLayer;
using Mole.Halt.ApplicationLayer.Internal;
using Mole.Halt.Utils;
using UnityEngine;

namespace Mole.Halt.Design
{
    [LevelDesign]
    public class LevelMaker : Level
    {
        [Header("Editor Tools")]
        [SerializeField] public LevelNumber number;
        [SerializeField] public LevelCategory category;
        [SerializeField] public LevelSource source;
    }
}