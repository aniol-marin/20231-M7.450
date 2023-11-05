using Mole.Halt.DataAccessLayer;
using Mole.Halt.DataLayer;

namespace Mole.Halt.GameLogicLayer
{
    public interface IFilterMatchingService
    {
        bool Match(Entity entity, EntityFilter filter);
    }
}