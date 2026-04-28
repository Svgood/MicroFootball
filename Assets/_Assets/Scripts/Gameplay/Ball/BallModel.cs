using MicroFootball.Configs;
using MicroFootball.Gameplay.View;
using UniRx;
using UnityEngine;

namespace MicroFootball.Gameplay.Model
{
    public enum GoalSide
    {
        None = 0,
        Left = 1,
        Right = 2
    }

    public sealed class BallModel
    {
        private readonly IGameplayPositionsProvider _gameplayPositionsProvider;
        private readonly float _linearDamping;
        private readonly float _radius;
        private readonly Vector2 _fieldSize;
        private readonly float _goalHalfHeight;

        public readonly ReactiveProperty<Vector2> Position;
        public readonly ReactiveProperty<Vector2> Velocity;

        public float Radius => _radius;

        public BallModel(GameplaySettings gameplaySettings, IGameplayPositionsProvider gameplayPositionsProvider)
        {
            _gameplayPositionsProvider = gameplayPositionsProvider;
            _radius = gameplaySettings.BallRadius;
            _linearDamping = gameplaySettings.BallLinearDamping;
            _fieldSize = gameplaySettings.FieldSize;
            _goalHalfHeight = gameplaySettings.GoalHalfHeight;
            Position = new ReactiveProperty<Vector2>(Vector2.zero);
            Velocity = new ReactiveProperty<Vector2>(Vector2.zero);
        }

        public void Kick(Vector2 direction, float force)
        {
            Velocity.Value += direction.normalized * force;
        }

        public void Tick(float dt)
        {
            Position.Value += Velocity.Value * dt;
            Velocity.Value = Vector2.Lerp(Velocity.Value, Vector2.zero, _linearDamping * dt);
            ClampInsidePitch();
        }

        public void Reset()
        {
            Position.Value = _gameplayPositionsProvider.BallStartingPosition;
            Velocity.Value = Vector2.zero;
        }

        public GoalSide GetGoalSide()
        {
            var halfWidth = _fieldSize.x * 0.5f;
            var ballPosition = Position.Value;

            if (Mathf.Abs(ballPosition.y) > _goalHalfHeight)
            {
                return GoalSide.None;
            }

            if (ballPosition.x <= -halfWidth + _radius)
            {
                return GoalSide.Left;
            }

            if (ballPosition.x >= halfWidth - _radius)
            {
                return GoalSide.Right;
            }

            return GoalSide.None;
        }

        private void ClampInsidePitch()
        {
            var halfWidth = _fieldSize.x * 0.5f - _radius;
            var halfHeight = _fieldSize.y * 0.5f - _radius;
            var position = Position.Value;
            var velocity = Velocity.Value;

            if (position.x <= -halfWidth || position.x >= halfWidth)
            {
                position.x = Mathf.Clamp(position.x, -halfWidth, halfWidth);
                velocity.x = -velocity.x;
            }

            if (position.y <= -halfHeight || position.y >= halfHeight)
            {
                position.y = Mathf.Clamp(position.y, -halfHeight, halfHeight);
                velocity.y = -velocity.y;
            }

            Position.Value = position;
            Velocity.Value = velocity;
        }
    }
}
