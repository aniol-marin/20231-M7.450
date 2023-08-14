using Mole.Halt.GameLogicLayer.AI;
using Mole.Halt.GameLogicLayer.AI.states;
using Mole.Halt.Utils.Interfaces;
using System;
using System.Collections.Generic;

namespace Mole.Halt.Utils.Factories
{
    /// <summary>
    /// Factory class for AI. Returns a ready-to-use dictionary.
    /// 
    /// Ensures that all the valid enumeration states have an implementation. Throws an exception otherwise.
    /// Given that the instances are injected allows custom rewiring through scene installer.
    /// </summary>
    public class BehaviorFactory
    {
        public Dictionary<AIState, IEnemyBehave> GetBehaviors(AIStateMachine controller)
        {
            Dictionary<AIState, IEnemyBehave> behaviors = new Dictionary<AIState, IEnemyBehave>(Enum.GetValues(typeof(AIState)).Length);
            IEnemyBehave behavior;

            foreach (AIState enumeration in Enum.GetValues(typeof(AIState)))
            {
                switch (enumeration)
                {
                    case AIState.invalid: continue;
                    case AIState.idle: behavior = new IdleBehavior(controller); break;
                    case AIState.alert: behavior = new AlertBehavior(controller); break;
                    case AIState.attack: behavior = new HostileBehavior(controller); break;
                    case AIState.retreat: behavior = new FleeingBehavior(controller); break;
                    case AIState.scan: behavior = new ScanningBehaviour(controller); break;
                    case AIState.patrol: behavior = new PatrollingBehavior(controller); break;
                    default: throw new NotImplementedException();
                }
                behaviors.Add(enumeration, behavior);
            }

            return behaviors;
        }
    }
}