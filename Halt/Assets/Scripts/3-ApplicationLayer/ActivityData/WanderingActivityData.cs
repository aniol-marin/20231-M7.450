using Mole.Halt.DataAccessLayer;
using Mole.Halt.GameLogicLayer;
using System;
using UnityEngine;

namespace Mole.Halt.ApplicationLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/Activities/Wandering")]

    public class WanderingActivityData : ProtoActivityData
    {
        public override Type activity => typeof(Wander);
    }
}