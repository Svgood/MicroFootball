using UnityEngine;

namespace MicroFootball.Gameplay.View
{
    public sealed class BallView : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;

        public Vector2 Position
        {
            set => _targetTransform.position = new Vector3(value.x, value.y, _targetTransform.position.z);
        }
    }
}
