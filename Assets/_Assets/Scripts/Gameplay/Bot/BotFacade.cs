namespace _Assets.Scripts.Gameplay.Bot
{
    public sealed class BotFacade
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
    }
}