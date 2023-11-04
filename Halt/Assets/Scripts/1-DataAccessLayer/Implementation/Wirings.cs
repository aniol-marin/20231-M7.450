using Mole.Halt.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Mole.Halt.DataAccessLayer.Internal
{
    [CreateAssetMenu(menuName = "Mole/Halt/Wiring")]
    public class Wirings : DataAsset
    {
        [SerializeField] private List<StateMachineWiring> wirings;

        public IEnumerable<StateMachineWiring> Data => wirings;
    }
}