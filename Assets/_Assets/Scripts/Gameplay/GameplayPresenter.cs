using _Assets.Scripts.Configs;
using UnityEngine;
using Zenject;

namespace _Assets.Scripts.Gameplay
{
    public sealed class GameplayPresenter : ITickable, IFixedTickable, IInitializable
    {
        private readonly GameplayModel _model;
        private readonly GameplayView _view;

        public GameplayPresenter(GameplayModel model, GameplayView view, GameplaySettings settings)
        {
            _model = model;
            _view = view;

            _view.Field.transform.localScale = new Vector3(settings.FieldSize.x, 1, settings.FieldSize.z);
        }

        public void Tick()
        {
            _model.Tick(Time.deltaTime);
        }

        public void FixedTick()
        {
            
        }

        public void Initialize()
        {
            
        }
    }
}
