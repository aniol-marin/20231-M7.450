using Mole.Halt.Utils;
using UnityEngine;

namespace Mole.Halt.PresentationLayer
{
    public interface IPrefabFactory
    {
        T Instantiate<T>(T prefab, Transform parent, bool prefabArgumentIntended = false) where T : NodeClass;
        T Instantiate<T>(Transform parent) where T : NodeClass;
    }
}