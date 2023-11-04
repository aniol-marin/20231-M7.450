using Mole.Halt.DataLayer;
using UnityEditor;
using UnityEngine;

namespace Mole.Halt.Design
{
    public abstract class GenericEntityWrapper<T> : EntityWrapper where T : Entity
    {
        protected T entity = null;

        public void Populate(T entity)
        {
            this.entity = entity;
            PopulateInternal();
        }

        public override void Rename(string newName)
        {
            name = newName;
        }

        public override void Store(ScriptableObject container)
        {
            AssetDatabase.AddObjectToAsset(this, container);
        }

        abstract protected void PopulateInternal();
    }
}