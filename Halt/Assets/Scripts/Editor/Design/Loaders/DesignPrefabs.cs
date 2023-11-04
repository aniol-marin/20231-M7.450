using Mole.Halt.PresentationLayer.Internal;
using System;
using UnityEngine;

namespace Mole.Halt.Design
{
    [CreateAssetMenu(fileName = "asset-design-prefabs", menuName = "Mole/Debug Prefabs Model")]

    public class DesignPrefabs : Prefabs
    {
        [SerializeField] private DebugEntityWidgetTile DebugEntityWidgetTile;

        public override T Get<T>()
        {
            Type type = typeof(T);

            return type.Name switch
            {
                nameof(DebugEntityWidgetTile) => DebugEntityWidgetTile as T,
                _ => base.Get<T>() as T,
            };
        }
    }
}