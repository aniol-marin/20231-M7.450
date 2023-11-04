using Mole.Halt.ApplicationLayer;
using UnityEngine;
using Zenject;

namespace Mole.Halt.Meta
{
    public class ApplicationIntraSceneInstaller : MonoInstaller
    {
        [SerializeField] private CameraProvider cameraProvider;
        [SerializeField] private SceneRootInitializer scene;

        public override void InstallBindings()
        {
            Container.BindInstance(cameraProvider);
            Container.BindInstance(scene);
        }
    }
}