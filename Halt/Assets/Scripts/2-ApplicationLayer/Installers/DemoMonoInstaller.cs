using Mole.Halt.GameLogicLayer.AI.states;
using Zenject;

namespace Mole.Halt.ApplicationLayer
{
    public class DemoMonoInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            // AI
            Container.Bind<IdleBehavior>().To<IdleBehavior>().AsTransient();
            Container.Bind<AlertBehavior>().To<AlertBehavior>().AsTransient();
            Container.Bind<FleeingBehavior>().To<FleeingBehavior>().AsTransient();
            Container.Bind<HostileBehavior>().To<HostileBehavior>().AsTransient();
        }
    }
}