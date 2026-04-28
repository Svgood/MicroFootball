using UnityEngine;

namespace MicroFootball.Configs
{
    [CreateAssetMenu(
        fileName = "GameplaySettings",
        menuName = "MicroFootball/Configs/Gameplay Settings")]
    public sealed class GameplaySettings : ScriptableObject
    {
        [Header("Field")]
        [SerializeField] private Vector3 _fieldSize = new Vector3(12f, 1f, 7f);
        [SerializeField] private float _goalHalfHeight = 1.5f;

        [Header("Bot")]
        [SerializeField] private float _botSpeed = 4f;
        [SerializeField] private float _botCollisionRadius = 0.6f;
        [SerializeField] private float _botCollisionKnockbackForce = 3.5f;
        [SerializeField] private float _botCollisionKnockbackDamping = 8f;
        [SerializeField] private float _botCollisionKnockbackCooldown = 0.12f;
        [SerializeField] private float _botKickRange = 0.8f;
        [SerializeField] private float _botKickForce = 8f;
        [SerializeField] private float _botKickCooldown = 0.35f;
        [SerializeField] private float _botKickHorizontalRandomAngle = 12f;
        [SerializeField] private float _botKickVerticalLift = 0.35f;
        [SerializeField] private float _botKickVerticalRandom = 0.1f;

        [Header("Ball")]
        [SerializeField] private float _ballRadius = 0.3f;
        [SerializeField] private float _ballLinearDamping = 1.4f;

        public Vector3 FieldSize => _fieldSize;
        public float GoalHalfHeight => _goalHalfHeight;
        public float BotSpeed => _botSpeed;
        public float BotCollisionRadius => _botCollisionRadius;
        public float BotCollisionKnockbackForce => _botCollisionKnockbackForce;
        public float BotCollisionKnockbackDamping => _botCollisionKnockbackDamping;
        public float BotCollisionKnockbackCooldown => _botCollisionKnockbackCooldown;
        public float BotKickRange => _botKickRange;
        public float BotKickForce => _botKickForce;
        public float BotKickCooldown => _botKickCooldown;
        public float BotKickHorizontalRandomAngle => _botKickHorizontalRandomAngle;
        public float BotKickVerticalLift => _botKickVerticalLift;
        public float BotKickVerticalRandom => _botKickVerticalRandom;
        public float BallRadius => _ballRadius;
        public float BallLinearDamping => _ballLinearDamping;
    }
}
