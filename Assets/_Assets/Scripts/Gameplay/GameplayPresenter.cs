using MicroFootball.Gameplay.Model;
using UnityEngine;
using Zenject;

namespace MicroFootball.Gameplay.Presenter
{
    public sealed class GameplayPresenter : ITickable
    {
        private readonly GameplayModel _model;

        public GameplayPresenter(GameplayModel model)
        {
            _model = model;
        }

        public void Tick()
        {
            _model.Tick(Time.deltaTime);
        }
    }
}
