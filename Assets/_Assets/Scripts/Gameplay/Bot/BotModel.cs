using MicroFootball.Configs;
using UniRx;
using UnityEngine;
using Zenject;

namespace MicroFootball.Gameplay.Model
{
    public class BotModelFactory : CustomFactory<BotInitDTO, BotModel, BotModel> { }

    public struct BotInitDTO
    {
        public BotInitDTO(Vector3 spawnPosition, Vector3 enemyGoalPosition)
        {
            SpawnPosition = spawnPosition;
            EnemyGoalPosition = enemyGoalPosition;
        }

        public Vector3 SpawnPosition { get; }
        public Vector3 EnemyGoalPosition { get; }
    }
    
    public sealed class BotModel
    {
        private readonly BallModel _ballModel;
        private readonly GameplaySettings _gameplaySettings;
        private readonly Vector2 _spawnPosition;
        private readonly float _speed;
        private float _kickCooldownLeft;

        public readonly ReactiveProperty<Vector2> Position;
        public Vector2 EnemyGoalPosition { get; }

        public BotModel(BotInitDTO botInitDto, BallModel ballModel, GameplaySettings gameplaySettings)
        {
            _spawnPosition = botInitDto.SpawnPosition;
            _speed = gameplaySettings.BotSpeed;
            _ballModel = ballModel;
            _gameplaySettings = gameplaySettings;
            
            EnemyGoalPosition = botInitDto.EnemyGoalPosition;
            Position = new ReactiveProperty<Vector2>(botInitDto.SpawnPosition);
        }

        public void Tick(float dt, Vector2 ballPosition)
        {
            var direction = ballPosition - Position.Value;
            if (direction.sqrMagnitude > 0.0001f)
            {
                Position.Value += direction.normalized * _speed * dt;
            }

            _kickCooldownLeft = Mathf.Max(0f, _kickCooldownLeft - dt);

            TryKick();
        }

        public bool CanKick(float kickRange, Vector2 ballPosition)
        {
            return _kickCooldownLeft <= 0f && Vector2.Distance(Position.Value, ballPosition) <= kickRange;
        }

        public void StartKickCooldown(float cooldownSeconds)
        {
            _kickCooldownLeft = cooldownSeconds;
        }

        public void TryKick()
        {
            if (!CanKick(_gameplaySettings.BotKickRange, _ballModel.Position.Value))
            {
                return;
            }

            var kickDirection = EnemyGoalPosition - _ballModel.Position.Value;
            if (kickDirection.sqrMagnitude <= 0.0001f)
            {
                return;
            }

            _ballModel.Kick(kickDirection, _gameplaySettings.BotKickForce);
            StartKickCooldown(_gameplaySettings.BotKickCooldown);
        }

        public void Reset()
        {
            Position.Value = _spawnPosition;
            _kickCooldownLeft = 0f;
        }
    }
}
