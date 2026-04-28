using UnityEngine;

namespace MicroFootball.Gameplay.View
{
    public sealed class BotView : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;

        public Vector2 Position
        {
            set
            {
                var target = _targetTransform != null ? _targetTransform : transform;
                target.position = new Vector3(value.x, value.y, target.position.z);
            }
        }
    }
}
