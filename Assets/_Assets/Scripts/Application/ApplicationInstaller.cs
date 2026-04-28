using MicroFootball.Application.Services;
using MicroFootball.Configs;
using UnityEngine;
using Zenject;

namespace MicroFootball.Application.Installers
{
    public sealed class ApplicationInstaller : MonoInstaller
    {
        [SerializeField] private ApplicationSettings _settings;

        public override void InstallBindings()
        {
            Container.BindInstance(_settings).IfNotBound();
            Container.BindInterfacesAndSelfTo<SceneService>().AsSingle().NonLazy();
        }
    }
}
