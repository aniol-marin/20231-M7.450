using Mole.Halt.ApplicationLayer;
using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System.Collections.Generic;
using UnityEngine;
using Object = Mole.Halt.DataLayer.Object;

namespace Mole.Halt.PresentationLayer
{
    public class ObjectView : ViewNode
    {
        [Injected] private readonly EntityMappingService mapping;
        [Injected] private readonly PostOffice post;
        [SerializeField] private Collider[] colliders;
        [SerializeField] private ObjectType type;

        public IEnumerable<Collider> Colliders => colliders;
        virtual protected Object Entity => new Object(type);

        // TEMP while testing
        private void Awake()
        {

            mapping.Add(Entity, colliders);
            post.FileNewEntity(Entity);
        }
    }
}