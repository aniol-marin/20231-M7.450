using Mole.Halt.ApplicationLayer;
using Mole.Halt.ApplicationLayer.Internal;
using Mole.Halt.Utils.Internal;
using UnityEngine;
using Zenject;

namespace Mole.Halt.Meta
{
    public class ApplicationNodeInstaller : MonoInstaller
    {
        [SerializeField] private MonoCoroutineProvider coroutines;

        public override void InstallBindings()
        {
            Container.Bind<Timeline>().AsSingle();
            Container.Bind<InitializablesProvider>().To<Initializables>().AsSingle();
        }
    }
}