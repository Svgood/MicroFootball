using _Assets.Scripts.Common;
using _Assets.Scripts.Configs;
using UniRx;
using Zenject;
using Object = UnityEngine.Object;

namespace _Assets.Scripts.Gameplay.Ball
{
    public sealed class BallPresenter : IInitializable
    {
        private readonly BallModel _model;
        private readonly BallView _view;
        private readonly CustomDisposable _customDisposable;

        public BallPresenter(BallModel model, PrefabsSettings prefabsSettings, CustomDisposable customDisposable)
        {
            _model = model;
            _view = Object.Instantiate(prefabsSettings.BallView);
            _customDisposable = customDisposable;
        }

        public void Initialize()
        {
            _model.Position
                .Subscribe(position => _view.Position = position)
                .AddTo(_customDisposable);
        }
    }
}
