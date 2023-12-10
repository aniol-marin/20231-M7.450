using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System;

namespace Mole.Halt.ApplicationLayer
{
    public abstract class ViewNode : NodeClass
    {
        virtual public EntityId Id { get => throw new NotImplementedException(); }
    }
}