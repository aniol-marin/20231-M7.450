using UnityEngine;
using Zenject;

namespace Mole.Halt.ApplicationLayer
{
    [CreateAssetMenu(menuName = "Mole/Management/Common Scriptable Installer")]
    public class CommonScriptableInstaller : ScriptableObjectInstaller<CommonScriptableInstaller>
    {
        public override void InstallBindings()
        {
            // Services 
            Container.Bind<ApplicationFinalizer>().To<SceneService>().AsCached();
            Container.Bind<SceneChanger>().To<SceneService>().AsCached();
            Container.Bind<SceneInitializer>().To<SceneService>().AsCached();
            Container.Bind<SceneFinalizer>().To<SceneService>().AsCached();
        }
    }
}