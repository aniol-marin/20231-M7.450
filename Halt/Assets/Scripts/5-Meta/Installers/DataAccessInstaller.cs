using Mole.Halt.DataAccessLayer;
using Mole.Halt.DataAccessLayer.Internal;
using UnityEngine;
using Zenject;

namespace Mole.Halt.Meta
{
    [CreateAssetMenu(menuName = "Mole/Management/Data Access Installer")]
    public class DataAccessInstaller : ScriptableObjectInstaller
    {
        [Header("Data Access Layer")]
        [SerializeField] private CharacterRepository characterRepository;
        [SerializeField] private ObjectRepository objectRepository;
        [SerializeField] private Wirings wiringData;

        public override void InstallBindings()
        {
            // Repositories
            Container.BindInstance(characterRepository).Lazy();
            Container.Bind<IObjectRepository>().FromInstance(objectRepository).Lazy();
            Container.BindInstance(wiringData).Lazy();
        }
    }
}