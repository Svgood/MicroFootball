using _Assets.Scripts.Common;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Gameplay.Ball;
using _Assets.Scripts.Gameplay.Bot;
using UnityEngine;
using Zenject;

namespace _Assets.Scripts.Gameplay
{
    public sealed class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private GameplaySettings _settings;
        [SerializeField] private PrefabsSettings _prefabsSettings;
        [SerializeField] private GameplayView _gameplayView;

        public override void InstallBindings()
        {
            Container.Rebind<CustomDisposable>().AsSingle();
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
