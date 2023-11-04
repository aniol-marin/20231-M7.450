using Mole.Halt.Utils;
using UnityEngine;

namespace Mole.Halt.DataAccessLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/Reactions/Condition")]
    public class StandardCondition : DataAsset
    {
        [SerializeField] private Condition condition;
        [SerializeField] private float deviation;
    }
}
