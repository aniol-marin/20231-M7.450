using Mole.Halt.ApplicationLayer;
using Mole.Halt.PresentationLayer;
using Mole.Halt.PresentationLayer.Internal;
using Mole.Halt.Utils;
using UnityEngine;
using Zenject;

namespace Mole.Halt.Meta
{
    public class PrefabFactory : IPrefabFactory
    {
        [Injected] private readonly DiContainer container;
        [Injected] private readonly Prefabs prefabs;
        [Injected] private readonly SceneInitializer initializer;

        public T Instantiate<T>(Transform parent) where T : NodeClass
        {
            return InstantiateInternal(prefabs.Get<T>(), parent);
        }

        public T Instantiate<T>(T prefab, Transform parent, bool prefabArgumentIntended = false) where T : NodeClass
        {
            if (!prefabArgumentIntended) Debug.LogWarning($"trying to instantiate {typeof(T).Name} ignoring the prefabs asset!");

            return InstantiateInternal(prefab, parent);
        }

        private T InstantiateInternal<T>(T prefab, Transform parent) where T : NodeClass
        {
            T instance = container.InstantiatePrefabForComponent<T>(prefab, parent);

            if (instance is Initializable i)
            {
                initializer.TryInitialize(i);
            }

            return instance;
        }
    }
}