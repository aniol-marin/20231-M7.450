using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System;
using UnityEngine;

namespace Mole.Halt.ApplicationLayer
{
    public class ColliderView : ViewNode
    {
        private event Callback<EntityId, Collider> onEnter = delegate { };
        private event Callback<EntityId, Collider> onStay = delegate { };
        private event Callback<EntityId, Collider> onExit = delegate { };
        private Func<EntityId> id;
        private bool initialized = false;

        public void Init(Func<EntityId> id, Callback<EntityId, Collider> onEnter, Callback<EntityId, Collider> onStay, Callback<EntityId, Collider> onExit)
        {
            this.id = id;
            this.onEnter = onEnter;
            this.onStay = onStay;
            this.onExit = onExit;

            initialized = true;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (!initialized)
            {
                //new Warning($"{transform.parent}/{name}");
                return;
            }
            onEnter(id(), collider);
        }
        private void OnTriggerStay(Collider collider)
        {
            if (!initialized)
            {
                //new Warning($"{transform.parent}/{name}");
                return;
            }
            onStay(id(), collider);
        }
        private void OnTriggerExit(Collider collider)
        {
            if (!initialized)
            {
                //new Warning($"{transform.parent}/{name}");
                return;
            }
            onExit(id(), collider);
        }
    }
}