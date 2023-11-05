using Mole.Halt.DataAccessLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;

namespace Mole.Halt.ApplicationLayer
{
    public class FilterMatchingService : IFilterMatchingService
    {
        public bool Match(Entity entity, EntityFilter filter)
        {
            // hardcoded for the assigment, to be expanded
            return entity is Bench && filter is ObjectFilter objectFilter && objectFilter.ObjectType == ObjectType.bench;
        }
    }
}