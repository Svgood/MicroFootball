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
            Container.BindInterfacesAndSelfTo<CustomDisposable>().AsSingle();
            Container.BindInstance(_settings).IfNotBound();
            Container.BindInterfacesAndSelfTo<SceneService>().AsSingle().NonLazy();
        }
    }
}
