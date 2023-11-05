using Mole.Halt.DataAccessLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mole.Halt.GameLogicLayer.Internal
{
    public class ActivityManager
    {
        [Injected] private readonly Allocator allocator;
        [Injected] private readonly IQueueManager queue;
        [Injected] private readonly IObjectRepository objects;

        public void StopActivity(Activity activity)
        {
            // TO DO
        }

        public Activity StartActivity(ProtoActivity data, EntityId effector)
        {
            Activity activity = data.activity.Name switch
            {
                nameof(Wander) => allocator.Instantiate<Wander>(),
                nameof(Patrol) => allocator.Instantiate<Patrol>(),
                _ => throw new NotImplementedException($"type {data.activity.Name} not implemented"),
            };
            activity.Initialize(effector);

            switch (activity.GetType().Name)
            {
                case nameof(Wander):
                    // no need for extra steps
                    break;
                case nameof(Patrol):
                    Patrol patrol = activity as Patrol;
                    IEnumerable<DataExchange> exchanges = objects
                        .GetAllOfType(ObjectType.path)
                        .Cast<Path>()
                        .Select(p => p.Data);
                    patrol.SetData(exchanges.ElementAt(RandomValue.Int(0, exchanges.Count())));
                    break;
                default: new Error($"entity {effector} trying to start a non-implemented activity: {activity.GetType().Name}"); break;
            }

            activity.Start();
            return activity;
        }
    }
}