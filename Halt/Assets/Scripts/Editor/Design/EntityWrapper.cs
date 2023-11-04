using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using UnityEngine;

namespace Mole.Halt.Design
{
    public abstract class EntityWrapper : DataAsset
    {
        abstract public Entity Entity { get; }

        abstract public void Rename(string newName);
        abstract public void Store(ScriptableObject container);
    }
}