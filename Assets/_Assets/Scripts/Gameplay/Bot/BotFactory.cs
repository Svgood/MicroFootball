using MicroFootball.Configs;
using MicroFootball.Gameplay.Model;
using MicroFootball.Gameplay.Presenter;
using MicroFootball.Gameplay.View;
using UnityEngine;
using Zenject;

namespace _Assets.Scripts.Gameplay.Bot
{
    public interface IBotFacadeFactory : IFactory<Vector3, Vector3, BotFacade> { }

    
    public sealed class BotFactory : IBotFacadeFactory
    {
        private readonly DiContainer _container;
        private readonly GameplaySettings _settings;
        private readonly PrefabsSettings _prefabsSettings;
        private readonly BotModelFactory _botModelFactory;
        private readonly BotPresenterFactory _botPresenterFactory;

        public BotFactory(DiContainer container, GameplaySettings settings, PrefabsSettings prefabsSettings)
        {
            _container = container;
            _settings = settings;
            _prefabsSettings = prefabsSettings;
            
            _botModelFactory = _container.Instantiate<BotModelFactory>();
            _botPresenterFactory = _container.Instantiate<BotPresenterFactory>();
        }
        
        public BotFacade Create(Vector3 spawnPosition, Vector3 enemyPosition)
        {
            var view = _container.InstantiatePrefabForComponent<BotView>(_prefabsSettings.BotView);
            var botInitDto = new BotInitDTO(spawnPosition, enemyPosition);
            var model = _botModelFactory.Create(botInitDto);
            var presenter = _botPresenterFactory.Create(model, view);

            return _container.Instantiate<BotFacade>(new object[] { model, presenter, view });
        }
    }
}
