using Mole.Halt.Utils;
using UnityEngine;
using Zenject;

namespace Mole.Halt.Meta
{
    [CreateAssetMenu(menuName = "Mole/Management/Utils Installer")]
    public class UtilsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Allocator>().To<CustomAllocator>().AsSingle();
            Container.Bind<Delay>().To<MonoBehaviorDelay>().AsSingle();
        }
    }
}