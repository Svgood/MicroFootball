using UnityEngine;

namespace MicroFootball.Gameplay.View
{
    public sealed class BotView : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;

        public Vector3 Position 
        {
            set => _targetTransform.position = value;
        }
    }
}
