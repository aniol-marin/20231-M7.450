using Mole.Halt.DataLayer;
using System;
using System.Collections.Generic;

namespace Mole.Halt.DataAccessLayer
{
    [Serializable]
    public class Reaction
    {
        public BehaviorType consequence;
        public List<Trigger> triggers;
        public List<StandardCondition> conditions;
        public List<EntityFilter> targets;
    }

    [Serializable]
    public struct Trigger
    {
        public EntityFilter trigger;
        public List<Condition> conditions;
    }

    [Serializable]
    public struct Condition
    {
        public ConditionType type;
        public int amount;
    }
    public enum ConditionType
    {
        inconditional = default,
        timeSeconds = 1,
        staminaLowerThan = 2,
        staminaHigherThan = 3,
        distance = 4,
        interactionRange = 5,
    }
}
