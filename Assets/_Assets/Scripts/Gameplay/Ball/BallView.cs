using UnityEngine;

namespace _Assets.Scripts.Gameplay.Ball
{
    public sealed class BallView : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;

        public Vector3 Position
        {
            set => _targetTransform.position = value;
        }
    }
}
