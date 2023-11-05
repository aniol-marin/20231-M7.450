using System;

namespace Mole.Halt.DataLayer
{
    [Serializable]
    public abstract class Activity
    {
        abstract public void Initialize(EntityId owner);
        abstract public void Start();
        abstract public void Stop();
    }
}