using Mole.Halt.DataAccessLayer;
using Mole.Halt.GameLogicLayer;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mole.Halt.ApplicationLayer
{
    [CreateAssetMenu(menuName = "Mole/Halt/Activities/Jogging")]

    public class JoggingActivityData : ProtoActivityData
    {
        public List<EntityFilter> targets;
        public override Type activity => typeof(Patrol);
    }
}