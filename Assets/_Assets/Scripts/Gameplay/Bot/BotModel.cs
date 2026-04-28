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
        private readonly float _knockbackDamping;
        private float _kickCooldownLeft;
        private Vector2 _knockbackVelocity;

        public readonly ReactiveProperty<Vector2> Position;
        public Vector2 EnemyGoalPosition { get; }

        public BotModel(BotInitDTO botInitDto, BallModel ballModel, GameplaySettings gameplaySettings)
        {
            _spawnPosition = new Vector2(botInitDto.SpawnPosition.x, botInitDto.SpawnPosition.z);
            _speed = gameplaySettings.BotSpeed;
            _knockbackDamping = gameplaySettings.BotCollisionKnockbackDamping;
            _ballModel = ballModel;
            _gameplaySettings = gameplaySettings;
            
            EnemyGoalPosition = new Vector2(botInitDto.EnemyGoalPosition.x, botInitDto.EnemyGoalPosition.z);
            Position = new ReactiveProperty<Vector2>(_spawnPosition);
        }

        public void Tick(float dt, Vector2 ballPosition)
        {
            Position.Value += _knockbackVelocity * dt;
            _knockbackVelocity = Vector2.Lerp(_knockbackVelocity, Vector2.zero, _knockbackDamping * dt);

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
            if (!CanKick(_gameplaySettings.BotKickRange, _ballModel.GroundPosition))
            {
                return;
            }

            var groundKickDirection = EnemyGoalPosition - _ballModel.GroundPosition;
            if (groundKickDirection.sqrMagnitude <= 0.0001f)
            {
                return;
            }

            var kickDirection = new Vector3(groundKickDirection.x, 0.35f, groundKickDirection.y);
            _ballModel.Kick(kickDirection, _gameplaySettings.BotKickForce);
            StartKickCooldown(_gameplaySettings.BotKickCooldown + Random.Range(-0.5f, 0.5f));
        }

        public void ApplyKnockback(Vector2 impulse)
        {
            _knockbackVelocity += impulse;
        }

        public void Reset()
        {
            Position.Value = _spawnPosition;
            _kickCooldownLeft = 0f;
            _knockbackVelocity = Vector2.zero;
        }
    }
}
