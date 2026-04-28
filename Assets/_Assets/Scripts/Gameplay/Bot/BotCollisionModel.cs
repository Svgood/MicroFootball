using MicroFootball.Configs;
using UnityEngine;

namespace MicroFootball.Gameplay.Model
{
    public sealed class BotCollisionModel
    {
        private const float Epsilon = 0.0001f;

        private readonly float _botCollisionRadius;
        private readonly float _knockbackForce;
        private readonly float _knockbackCooldown;
        private readonly Vector2 _fieldSize;
        private float _knockbackCooldownLeft;

        public BotCollisionModel(GameplaySettings gameplaySettings)
        {
            _botCollisionRadius = gameplaySettings.BotCollisionRadius;
            _knockbackForce = gameplaySettings.BotCollisionKnockbackForce;
            _knockbackCooldown = gameplaySettings.BotCollisionKnockbackCooldown;
            _fieldSize = gameplaySettings.FieldSize;
        }

        public void Resolve(BotModel firstBot, BotModel secondBot, float dt)
        {
            _knockbackCooldownLeft = Mathf.Max(0f, _knockbackCooldownLeft - dt);

            var firstPosition = firstBot.Position.Value;
            var secondPosition = secondBot.Position.Value;
            var delta = secondPosition - firstPosition;
            var distance = delta.magnitude;
            var minDistance = _botCollisionRadius * 2f;

            if (distance >= minDistance)
            {
                return;
            }

            var separationDirection = distance > Epsilon ? delta / distance : Vector2.right;
            var penetrationDepth = minDistance - distance;
            var correction = separationDirection * (penetrationDepth * 0.5f);

            firstPosition -= correction;
            secondPosition += correction;

            firstBot.Position.Value = ClampInsidePitch(firstPosition);
            secondBot.Position.Value = ClampInsidePitch(secondPosition);

            if (_knockbackCooldownLeft > 0f)
            {
                return;
            }

            var randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            var randomDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle));
            if (randomDirection.sqrMagnitude <= Epsilon)
            {
                randomDirection = separationDirection;
            }

            firstBot.ApplyKnockback(randomDirection.normalized * _knockbackForce);
            secondBot.ApplyKnockback(-randomDirection.normalized * _knockbackForce);
            _knockbackCooldownLeft = _knockbackCooldown;
        }

        private Vector2 ClampInsidePitch(Vector2 position)
        {
            var halfWidth = _fieldSize.x * 0.5f - _botCollisionRadius;
            var halfHeight = _fieldSize.y * 0.5f - _botCollisionRadius;
            return new Vector2(
                Mathf.Clamp(position.x, -halfWidth, halfWidth),
                Mathf.Clamp(position.y, -halfHeight, halfHeight));
        }
    }
}
