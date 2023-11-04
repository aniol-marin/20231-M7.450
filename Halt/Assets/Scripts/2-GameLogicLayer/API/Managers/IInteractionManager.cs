using Mole.Halt.DataLayer;
using Mole.Halt.Utils;

namespace Mole.Halt.GameLogicLayer
{
    public interface IInteractionManager
    {
        event Callback<EntityId, Entity> OnInteractionReported;

        void AddBehaviorManager(BehaviorManager manager);
        void RemoveBehaviorManager(BehaviorManager manager);
    }
}