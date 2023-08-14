using Mole.Halt.PresentationLayer;
using UnityEngine;
using Zenject;

namespace Mole.Halt.ApplicationLayer
{
    public class MenuMonoInstaller : MonoInstaller
    {
        [SerializeField] private ScreensData screensData;

        public override void InstallBindings()
        {
            // Data holders
            Container.BindInstance<ScreensData>(screensData).Lazy();
        }
    }
}