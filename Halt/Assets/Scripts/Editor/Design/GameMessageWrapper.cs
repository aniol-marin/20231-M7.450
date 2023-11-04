using Mole.Halt.GameLogicLayer;
using Mole.Halt.Utils;

namespace Mole.Halt.Design
{
    public abstract class GameMessageWrapper : DataAsset
    {
        abstract public GameEvent GameEvent { get; }

        abstract public void Rename(string newName);
        abstract public void Store(History container);
    }
}