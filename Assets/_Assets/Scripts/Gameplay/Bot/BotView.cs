using UnityEngine;

namespace _Assets.Scripts.Gameplay.Bot
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
