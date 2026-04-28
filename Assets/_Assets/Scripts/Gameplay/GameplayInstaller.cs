using _Assets.Scripts.Gameplay.Bot;
using MicroFootball.Configs;
using MicroFootball.Gameplay.Model;
using MicroFootball.Gameplay.Presenter;
using MicroFootball.Gameplay.View;
using UnityEngine;
using Zenject;

namespace MicroFootball.Gameplay.Installers
{
    public sealed class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplaySettings _settings;
        [SerializeField] private PrefabsSettings _prefabsSettings;
        [SerializeField] private GameplayView _gameplayView;

        public override void InstallBindings()
        {
            Container.BindInstance(_settings);
            Container.BindInstance(_prefabsSettings);
            Container.BindInterfacesAndSelfTo<GameplayView>().FromInstance(Instantiate(_gameplayView));

            Container.Bind<IBotFacadeFactory>().To<BotFactory>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameplayModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BotCollisionModel>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<BallModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<BallPresenter>().AsSingle().NonLazy();
        }
    }
}
