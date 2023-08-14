using UnityEngine;
using Zenject;

namespace Mole.Halt.ApplicationLayer
{
    public class CommonMonoInstaller : MonoInstaller
    {
        [SerializeField] private ScenesData scenesData;
        [SerializeField] private PrefabProvider prefabsData;

        public override void InstallBindings()
        {
            // Data holders
            Container.BindInstance<ScenesData>(scenesData).Lazy();
            Container.BindInstance<PrefabProvider>(prefabsData).Lazy();
        }
    }
}