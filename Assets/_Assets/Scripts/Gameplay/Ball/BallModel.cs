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
        private readonly Vector3 _fieldSize;
        private readonly float _goalHalfHeight;

        private const float Gravity = 9.81f;
        private const float WallRestitution = 0.85f;
        private const float GroundRestitution = 0.55f;
        private const float MinBounceVelocity = 0.2f;

        public readonly ReactiveProperty<Vector3> Position;
        public readonly ReactiveProperty<Vector3> Velocity;
        public Vector3 GroundPosition => new Vector3(Position.Value.x, 0f, Position.Value.z);

        public float Radius => _radius;

        public BallModel(GameplaySettings gameplaySettings, IGameplayPositionsProvider gameplayPositionsProvider)
        {
            _gameplayPositionsProvider = gameplayPositionsProvider;
            _radius = gameplaySettings.BallRadius;
            _linearDamping = gameplaySettings.BallLinearDamping;
            _fieldSize = gameplaySettings.FieldSize;
            _goalHalfHeight = gameplaySettings.GoalHalfHeight;
            Position = new ReactiveProperty<Vector3>(Vector3.zero);
            Velocity = new ReactiveProperty<Vector3>(Vector3.zero);
        }

        public void Kick(Vector3 direction, float force)
        {
            if (direction.sqrMagnitude <= 0.0001f)
            {
                return;
            }

            Velocity.Value += direction.normalized * force;
        }

        public void Tick(float dt)
        {
            var velocity = Velocity.Value;
            velocity += Vector3.down * Gravity * dt;

            var horizontalVelocity = new Vector3(velocity.x, 0f, velocity.z);
            horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, _linearDamping * dt);
            velocity.x = horizontalVelocity.x;
            velocity.z = horizontalVelocity.z;

            Position.Value += velocity * dt;
            Velocity.Value = velocity;
            ClampInsidePitch();
        }

        public void Reset()
        {
            var startPosition = _gameplayPositionsProvider.BallStartingPosition;
            startPosition.y = Mathf.Max(_radius, startPosition.y);
            Position.Value = startPosition;
            Velocity.Value = Vector3.zero;
        }

        public GoalSide GetGoalSide()
        {
            var ballPosition = Position.Value;
            var leftGoalPosition = _gameplayPositionsProvider.Bot1StartingPosition;
            var rightGoalPosition = _gameplayPositionsProvider.Bot2StartingPosition;
            var leftGoalX = Mathf.Min(leftGoalPosition.x, rightGoalPosition.x);
            var rightGoalX = Mathf.Max(leftGoalPosition.x, rightGoalPosition.x);

            if (Mathf.Abs(ballPosition.z - leftGoalPosition.z) <= _goalHalfHeight &&
                ballPosition.x <= leftGoalX + _radius)
            {
                return GoalSide.Left;
            }

            if (Mathf.Abs(ballPosition.z - rightGoalPosition.z) <= _goalHalfHeight &&
                ballPosition.x >= rightGoalX - _radius)
            {
                return GoalSide.Right;
            }

            return GoalSide.None;
        }

        private void ClampInsidePitch()
        {
            var halfWidth = _fieldSize.x * 0.5f - _radius;
            var halfHeight = _fieldSize.z * 0.5f - _radius;
            var position = Position.Value;
            var velocity = Velocity.Value;

            if (position.x <= -halfWidth || position.x >= halfWidth)
            {
                position.x = Mathf.Clamp(position.x, -halfWidth, halfWidth);
                velocity.x = -velocity.x * WallRestitution;
            }

            if (position.z <= -halfHeight || position.z >= halfHeight)
            {
                position.z = Mathf.Clamp(position.z, -halfHeight, halfHeight);
                velocity.z = -velocity.z * WallRestitution;
            }

            if (position.y <= _radius)
            {
                position.y = _radius;
                if (velocity.y < 0f)
                {
                    velocity.y = -velocity.y * GroundRestitution;
                    if (velocity.y < MinBounceVelocity)
                    {
                        velocity.y = 0f;
                    }
                }
            }

            Position.Value = position;
            Velocity.Value = velocity;
        }
    }
}
