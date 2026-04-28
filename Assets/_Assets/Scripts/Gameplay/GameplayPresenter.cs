using MicroFootball.Configs;
using MicroFootball.Gameplay.Model;
using MicroFootball.Gameplay.View;
using UnityEngine;
using Zenject;

namespace MicroFootball.Gameplay.Presenter
{
    public sealed class GameplayPresenter : ITickable
    {
        private readonly GameplayModel _model;
        private readonly GameplayView _view;

        public GameplayPresenter(GameplayModel model, GameplayView view, GameplaySettings settings)
        {
            _model = model;
            _view = view;

            _view.Field.transform.localScale = new Vector3(settings.FieldSize.x, 1, settings.FieldSize.y);
        }

        public void Tick()
        {
            _model.Tick(Time.deltaTime);
        }
    }
}
