using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;

namespace Mole.Halt.GameLogicLayer
{
    public interface ICensusManager
    {
        event Callback<EntityId> OnCharacterRegistered;

        public IEnumerable<EntityId> Active { get; }
        void AddBehaviorManager(BehaviorManager manager);
        void RemoveBehaviorManager(BehaviorManager manager);
    }
}