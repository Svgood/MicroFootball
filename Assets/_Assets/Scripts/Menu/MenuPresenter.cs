using MicroFootball.Application.Services;
using MicroFootball.Configs;
using MicroFootball.Menu.Model;
using MicroFootball.Menu.View;
using System;
using UniRx;
using Zenject;
using Object = UnityEngine.Object;

namespace MicroFootball.Menu.Presenter
{
    public sealed class MenuPresenter : IInitializable, IDisposable
    {
        private readonly MenuModel _model;
        private readonly MenuView _view;
        private readonly MenuSettings _settings;
        private readonly ISceneService _sceneService;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public MenuPresenter(
            MenuModel model,
            MenuView view,
            MenuSettings settings,
            ISceneService sceneService)
        {
            _model = model;
            _view = Object.Instantiate(view);
            _settings = settings;
            _sceneService = sceneService;
        }

        public void Initialize()
        {
            _view.StartClicked
                .Subscribe(_ => _model.RequestStart())
                .AddTo(_disposables);

            _model.StartRequested
                .Subscribe(_ => _sceneService.LoadGameplay())
                .AddTo(_disposables);

            if (_settings.Autostart)
            {
                Observable.Timer(TimeSpan.FromSeconds(_settings.AutostartDelaySeconds))
                    .Subscribe(_ => _model.RequestStart())
                    .AddTo(_disposables);
            }
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
