using System;
using MicroFootball.Gameplay.Model;
using MicroFootball.Gameplay.View;
using UniRx;
using Zenject;

namespace _Assets.Scripts.Gameplay.Bot
{
    public class BotPresenterFactory : CustomFactory<BotModel, BotView, BotPresenter, BotPresenter> { }
    
    public sealed class BotPresenter : IDisposable
    {
        private readonly BotModel _model;
        private readonly BotView _view;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public BotPresenter(BotModel model, BotView view)
        {
            _model = model;
            _view = view;
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
