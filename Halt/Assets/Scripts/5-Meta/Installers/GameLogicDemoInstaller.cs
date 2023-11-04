using Mole.Halt.ApplicationLayer;
using Mole.Halt.GameLogicLayer;
using Mole.Halt.GameLogicLayer.Internal;
using Mole.Halt.Utils;
using Mole.Halt.Utils.Internal;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Mole.Halt.Meta
{
    [CreateAssetMenu(menuName = "Mole/Management/Game Logic Demo Installer")]
    public class GameLogicDemoInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICensusManager>().To<CensusManager>().AsSingle().NonLazy();
            Container.Bind<InteractionManager>().AsSingle();
            Container.Bind<ActivityManager>().AsSingle();
            Container.Bind<IQueueManager>().To<QueueManager>().AsSingle();
            Container.Bind<TickProvider>().FromInstance(FindObjectsByType<MonoCoroutineProvider>(sortMode: FindObjectsSortMode.None).First()).AsSingle();
            Container.Bind<EntityMappingService>().AsSingle();
            InstallAI();
        }

        private void InstallAI()
        {
            Container.Bind<BehaviorFactory>().AsSingle();

            // Default value
            Container.Bind<BehaviorManager>().WithId(ControllerType.mock).To<MockBehaviorManager>().AsTransient();

            // All the values
            Container.Bind<BehaviorManager>().WithId(ControllerType.stateMachine).To<BehaviorStateMachine>().AsTransient();
        }
    }
}