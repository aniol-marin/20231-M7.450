using Mole.Halt.PresentationLayer;
using UnityEngine;
using Zenject;

namespace Mole.Halt.Meta
{
    [CreateAssetMenu(menuName = "Mole/Management/Installers/Presentation Demo")]
    public class PresentationDemoInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CharacterPrefabLoader characterPrefabs;
        [SerializeField] private ScreensData screensData;

        public override void InstallBindings()
        {
            // Presentation
            Container.Bind<BehaviorStrategy>().AsTransient();
            Container.Bind<Navigation>().AsSingle();
            Container.BindInstance(screensData).Lazy();

            // Loaders
            Container.Bind<ILevelLoader>().To<LevelLoader>().AsSingle();
            Container.BindInstance(characterPrefabs);

            // Factories
            Container.Bind<IPrefabFactory>().To<PrefabFactory>().AsSingle();
            Container.Bind<ICharactersFactory>().To<CharactersFactory>().AsSingle();
        }
    }
}