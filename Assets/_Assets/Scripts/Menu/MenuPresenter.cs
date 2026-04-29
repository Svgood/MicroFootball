using System;
using _Assets.Scripts.Common;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Services;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace _Assets.Scripts.Menu
{
    public class PanelPresenter<T>
    {
    }
    
    public sealed class MenuPresenter : PanelPresenter<MenuView>, IInitializable
    {
        private readonly MenuModel _model;
        private readonly MenuView _view;
        private readonly MenuSettings _settings;
        private readonly ISceneService _sceneService;
        private readonly CustomDisposable _customDisposable;
        private readonly CompositeDisposable _compositeDisposable = new CompositeDisposable();
        
        public MenuPresenter(
            MenuModel model,
            MenuView view,
            MenuSettings settings,
            ISceneService sceneService,
            CustomDisposable customDisposable,
            SignalBus signalBus)
        {
            _model = model;
            _view = Object.Instantiate(view);
            _settings = settings;
            _sceneService = sceneService;
            _customDisposable = customDisposable;
        }

        public void Initialize()
        {
            _view.StartClicked
                .Subscribe(_ => _model.RequestStart())
                .AddTo(_compositeDisposable);

            _model.StartRequested
                .Subscribe(_ => _sceneService.LoadGameplay())
                .AddTo(_compositeDisposable);

            if (_settings.Autostart)
            {
                Observable.Timer(TimeSpan.FromSeconds(_settings.AutostartDelaySeconds))
                    .Subscribe(_ => _model.RequestStart())
                    .AddTo(_compositeDisposable);
            }

            _customDisposable.OnDisposal(() => Debug.Log("MenuPresenter disposed"));
        }
    }


}
