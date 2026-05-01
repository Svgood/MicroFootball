using _Assets.Scripts.Common;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Services;
using UnityEngine;
using Zenject;

namespace _Assets.Scripts.Application
{
    public sealed class ApplicationInstaller : MonoInstaller
    {
        [SerializeField] private ApplicationSettings _settings;

        public override void InstallBindings()
        {
            Container.BindInstance(_settings);
            
            Container.BindInterfacesAndSelfTo<CustomDisposable>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneService>().AsSingle().NonLazy();
        }
    }
}
