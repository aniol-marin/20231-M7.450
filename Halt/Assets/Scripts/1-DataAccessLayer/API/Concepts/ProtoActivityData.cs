using Mole.Halt.DataLayer;
using Mole.Halt.Utils;
using System;

namespace Mole.Halt.DataAccessLayer
{
    public abstract class ProtoActivityData : DataAsset, ProtoActivity
    {
        abstract public Type activity { get; }
    }
}