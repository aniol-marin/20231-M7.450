using System;

namespace Mole.Halt.DataLayer
{
    public abstract class Entity
    {
        public readonly EntityId Id;
        public readonly EntityType EntityType;

        protected Entity(EntityType entityType)
        {
            Id = (EntityId)(++idCount);
            EntityType = entityType;
        }
        private static uint idCount = 0;

        /// <summary>
        /// For restoring previous games only
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="id"></param>
        protected Entity(EntityType entityType, EntityId id)
        {
            Id = id;
            EntityType = entityType;
            idCount = Math.Max(idCount, (uint)id);
        }
    }
}