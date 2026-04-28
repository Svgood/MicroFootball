using System;
using _Assets.Scripts.Gameplay.Bot;
using MicroFootball.Gameplay.Presenter;
using MicroFootball.Gameplay.View;

namespace MicroFootball.Gameplay.Model
{
    public sealed class BotFacade : IDisposable
    {
        private readonly BotPresenter _presenter;

        public BotModel Model { get; }
        public BotView View { get; }

        public BotFacade(BotModel model, BotPresenter presenter, BotView view)
        {
            Model = model;
            _presenter = presenter;
            View = view;

            _presenter.Initialize();
        }

        public void Dispose()
        {
            _presenter.Dispose();
        }
    }
}