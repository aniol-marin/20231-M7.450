using Mole.Halt.DataLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;

namespace Mole.Halt.ApplicationLayer
{
    public class PostOffice
    {
        [Injected] private readonly IQueueManager queueManager;

        public void FileNewEntity(Entity entity)
        {
            queueManager.ReportEvent(new RegistrationEvent(entity));
        }
    }

    public enum RequestType
    {
        none = default,
        characters,
    }
}