using Mole.Halt.DataAccessLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Mole.Halt.GameLogicLayer.Internal
{
    public class ActivityManager
    {
        [Injected] private readonly IQueueManager queue;
        [Injected] private readonly IObjectRepository objects;

        public void StartActivity(IActivity activity, EntityId effector)
        {
            ActivityData data = activity as ActivityData;
            switch (activity.activity.GetType().Name)
            {
                case nameof(Patrol): StartPatrol(activity as Patrol, effector, data.targets); break;
                default: new Error($"entity {effector} trying to start a non-implemented activity: {activity.GetType().Name}"); break;
            }
        }

        public void StartPatrol(Patrol patrol, EntityId effector, IEnumerable<EntityFilter> targets)
        {
            // TO DO handle random targets
            EntityId target = objects.GetAllOfType(ObjectType.path).First().Id;
            bool exists = objects.TryGet(target, out Path path);
                
            if (exists)
            {
                queue.ReportEvent(new OrderEvent(OrderType.reach, effector, path.ClosestPoint()));
            }
            else
            {
                new Error($"Entity {effector} trying to patrol towards an invalid path ({path})");
            }
        }
    }
}