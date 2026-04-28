using MicroFootball.Gameplay.Model;
using MicroFootball.Gameplay.View;
using System;
using MicroFootball.Configs;
using UniRx;
using Zenject;
using Object = UnityEngine.Object;

namespace MicroFootball.Gameplay.Presenter
{
    public sealed class BallPresenter : IInitializable, IDisposable
    {
        private readonly BallModel _model;
        private readonly BallView _view;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public BallPresenter(BallModel model, PrefabsSettings prefabsSettings)
        {
            _model = model;
            _view = Object.Instantiate(prefabsSettings.BallView);
        }

        public void Initialize()
        {
            _model.Position
                .Subscribe(position => _view.Position = position)
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
