using Mole.Halt.DataAccessLayer;
using Mole.Halt.GameLogicLayer;
using System;
using UnityEngine;

namespace Mole.Halt.ApplicationLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/Activities/Resting")]

    public class RestingActivityData : ProtoActivityData
    {
        [SerializeField] private EntityFilter filter;
        public override Type activity => typeof(Rest);
    }
}