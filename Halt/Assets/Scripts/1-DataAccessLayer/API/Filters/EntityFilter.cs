using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System;

namespace Mole.Halt.DataAccessLayer
{
    public abstract class EntityFilter : DataAsset
    {
        abstract public EntityType Entity { get; }
        abstract public Entity Clone(EntityId id);


        public static bool operator !=(EntityFilter left, EntityFilter right) => !(left == right);
        public static bool operator ==(EntityFilter left, EntityFilter right) => left.Entity == right.Entity;
        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), name, hideFlags, Entity);
        public override bool Equals(object obj) => obj is EntityFilter filter && Entity == filter.Entity;
    }
}
