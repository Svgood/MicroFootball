using _Assets.Scripts.Common;
using _Assets.Scripts.Configs;
using _Assets.Scripts.Gameplay.Ball;
using UniRx;
using UnityEngine;

namespace _Assets.Scripts.Gameplay.Bot
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
        private readonly Vector3 _spawnPosition;
        private readonly float _speed;
        private readonly float _knockbackDamping;
        private float _kickCooldownLeft;
        private Vector3 _knockbackVelocity;

        public readonly ReactiveProperty<Vector3> Position;
        public Vector3 EnemyGoalPosition { get; }

        public BotModel(BotInitDTO botInitDto, BallModel ballModel, GameplaySettings gameplaySettings)
        {
            _spawnPosition = botInitDto.SpawnPosition;
            _speed = gameplaySettings.BotSpeed;
            _knockbackDamping = gameplaySettings.BotCollisionKnockbackDamping;
            _ballModel = ballModel;
            _gameplaySettings = gameplaySettings;
            
            EnemyGoalPosition = botInitDto.EnemyGoalPosition;
            Position = new ReactiveProperty<Vector3>(_spawnPosition);
        }

        public void Tick(float dt, Vector3 ballPosition)
        {
            Position.Value += _knockbackVelocity * dt;
            _knockbackVelocity = Vector3.Lerp(_knockbackVelocity, Vector3.zero, _knockbackDamping * dt);

            var direction = new Vector3(
                ballPosition.x - Position.Value.x,
                0f,
                ballPosition.z - Position.Value.z);
            if (direction.sqrMagnitude > 0.0001f)
            {
                Position.Value += direction.normalized * _speed * dt;
            }

            var position = Position.Value;
            position.y = 0.4f;
            Position.Value = position;

            _kickCooldownLeft = Mathf.Max(0f, _kickCooldownLeft - dt);

            TryKick();
        }

        public bool CanKick(float kickRange, Vector3 ballPosition)
        {
            var delta = new Vector3(
                ballPosition.x - Position.Value.x,
                0f,
                ballPosition.z - Position.Value.z);
            return _kickCooldownLeft <= 0f && delta.magnitude <= kickRange;
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

            var groundKickDirection = new Vector3(
                EnemyGoalPosition.x - _ballModel.GroundPosition.x,
                0f,
                EnemyGoalPosition.z - _ballModel.GroundPosition.z);
            if (groundKickDirection.sqrMagnitude <= 0.0001f)
            {
                return;
            }

            var randomizedGroundDirection = ApplyHorizontalSpread(groundKickDirection.normalized);
            var verticalKick = _gameplaySettings.BotKickVerticalLift +
                               Random.Range(-_gameplaySettings.BotKickVerticalRandom, _gameplaySettings.BotKickVerticalRandom);
            var kickDirection = new Vector3(randomizedGroundDirection.x, Mathf.Max(0f, verticalKick), randomizedGroundDirection.z);
            _ballModel.Kick(kickDirection, _gameplaySettings.BotKickForce);
            StartKickCooldown(_gameplaySettings.BotKickCooldown + Random.Range(-0.5f, 0.5f));
        }

        public void ApplyKnockback(Vector3 impulse)
        {
            impulse.y = 0f;
            _knockbackVelocity += impulse;
        }

        private Vector3 ApplyHorizontalSpread(Vector3 direction)
        {
            var randomAngle = Random.Range(
                -_gameplaySettings.BotKickHorizontalRandomAngle,
                _gameplaySettings.BotKickHorizontalRandomAngle);
            return Quaternion.Euler(0f, randomAngle, 0f) * direction;
        }

        public void Reset()
        {
            Position.Value = _spawnPosition;
            _kickCooldownLeft = 0f;
            _knockbackVelocity = Vector3.zero;
        }
    }
}
