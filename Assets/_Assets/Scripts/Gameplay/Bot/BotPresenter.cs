using _Assets.Scripts.Common;
using UniRx;
using Zenject;

namespace _Assets.Scripts.Gameplay.Bot
{
    public class BotPresenterFactory : CustomFactory<BotModel, BotView, BotPresenter, BotPresenter> { }
    
    public sealed class BotPresenter
    {
        private readonly BotModel _model;
        private readonly BotView _view;
        private readonly CustomDisposable _customDisposable;

        public BotPresenter(BotModel model, BotView view, CustomDisposable customDisposable)
        {
            _model = model;
            _view = view;
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
