using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mole.Halt.ApplicationLayer
{
    public class EntityMappingService
    {
        private readonly Dictionary<Collider, Entity> collideables = new();
        private readonly Dictionary<EntityId, ViewNode> views = new();
        private readonly Dictionary<Path, Position> paths = new();

        public void Add(Entity entity, IEnumerable<Collider> colliders)
        {
            colliders.ForEach(c => collideables[c] = entity);
        }

        public void Add(Character character, ViewNode view)
        {
            views.Add(character.Id, view);
        }

        public void Add(Path entity, Position origin)
        {
            paths.Add(entity, origin);
        }

        public void Remove(Entity entity)
        {
            collideables
                .ToList()
                .Where(e => e.Value.Id == entity.Id)
                .Select(p => p.Key)
                .ForEach(c => collideables.Remove(c));
        }

        public bool TryResolve(Collider collider, out Entity entity)
        {
            return collideables.TryGetValue(collider, out entity);
        }

        public ViewNode GetView(EntityId id)
        {
            return views.TryGetValue(id, out ViewNode view) ? view : throw new Exception("inexistent entity view");
        }

        public CharacterType GetCharacterType(ViewNode view)
        {
            return collideables
                .Values
                .Where(e => e.Id == view.Id)
                .Cast<Character>()
                .FirstOrDefault()?
                .CharacterType
                ?? default;
        }

        public Position GetPathOrigin(Path path) => paths.GetValueOrDefault(path);
    }
}