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

        // TEMP while testing
        private void Awake()
        {
            Object bench = new(type);

            mapping.Add(bench, colliders);
            post.FileNewEntity(bench);
        }
    }
}