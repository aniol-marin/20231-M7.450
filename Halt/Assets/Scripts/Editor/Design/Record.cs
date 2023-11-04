using Mole.Halt.GameLogicLayer;
using UnityEditor;

namespace Mole.Halt.Design
{
    public abstract class Record<T> : GameMessageWrapper where T :  class, GameEvent
    {
        protected T @event = null;

        public void Populate(T gameEvent, History container)
        {
            @event = gameEvent;
            PopulateInternal(container);
        }

        public override void Rename(string newName)
        {
            name = newName;
        }

        public override void Store(History container)
        {
            AssetDatabase.AddObjectToAsset(this, container);
        }

        abstract protected void PopulateInternal(History container);
    }
}