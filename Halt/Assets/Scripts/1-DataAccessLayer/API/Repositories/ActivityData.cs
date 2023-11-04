using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;

namespace Mole.Halt.DataAccessLayer
{
    public abstract class ActivityData : DataAsset, IActivity
    {
        abstract public Activity activity { get; }
        public List<EntityFilter> targets;
    }
}