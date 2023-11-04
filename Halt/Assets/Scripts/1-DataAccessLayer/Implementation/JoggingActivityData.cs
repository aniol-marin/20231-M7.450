using Mole.Halt.DataLayer;
using UnityEngine;

namespace Mole.Halt.DataAccessLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/Activities/Jogging")]

    public class JoggingActivityData : ActivityData
    {
        [SerializeField] private EntityFilter filter;
        private Activity jogging;
        public override Activity activity => jogging ??= new Patrol();
    }
}