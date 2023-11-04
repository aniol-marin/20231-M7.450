using Mole.Halt.ApplicationLayer;
using Mole.Halt.ApplicationLayer.Internal;
using Mole.Halt.DataAccessLayer;
using Mole.Halt.PresentationLayer;
using Mole.Halt.PresentationLayer.Internal;
using UnityEngine;
using Zenject;

namespace Mole.Halt.Meta
{
    [CreateAssetMenu(menuName = "Mole/Management/Application Installer")]
    public class ApplicationAssetInstaller : ScriptableObjectInstaller
    {
        [Header("Application Layer")]
        [SerializeField] private Levels levelsData;
        [SerializeField] private Prefabs prefabsData;
        [SerializeField] private Scenes scenesData;
        [SerializeField] private CensusPackage census;

        public override void InstallBindings()
        {
            // Data holders
            Container.BindInstance(scenesData).Lazy();
            Container.BindInstance(prefabsData).Lazy();
            Container.BindInstance(levelsData).Lazy();
            Container.Bind<ICensusPackage>().FromInstance(census).Lazy();

            // Utils
            Container.Bind<InputActionsInstaller>().AsSingle();
            Container.Bind<PostOffice>().AsSingle();
            Container.Bind<ApplicationFinalizer>().To<SceneService>().AsCached();
            Container.Bind<SceneChanger>().To<SceneService>().AsCached();
            Container.Bind<SceneInitializer>().To<SceneService>().AsCached();
            Container.Bind<SceneFinalizer>().To<SceneService>().AsCached();
            Container.Bind<IUserInputService>().To<UserInputService>().AsSingle();
            Container.Bind<EventParser>().To<EventParser>().AsSingle();

            // Message Handlers
            Container.Bind<MessageHandler>().WithId(MessageHandlerType.characters).To<CharactersMessageHandler>().AsSingle();
        }
    }
}