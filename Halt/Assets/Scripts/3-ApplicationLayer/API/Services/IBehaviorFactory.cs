using Mole.Halt.DataAccessLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.Utils;

namespace Mole.Halt.ApplicationLayer.Internal
{
    public interface IBehaviorFactory
    {
        BehaviorManager Get(ControllerType managerType);
        StateMachineWiring GetWiring(Character character);
    }
}