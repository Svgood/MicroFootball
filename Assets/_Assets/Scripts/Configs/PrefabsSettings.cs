using _Assets.Scripts.Gameplay.Ball;
using _Assets.Scripts.Gameplay.Bot;
using UnityEngine;

namespace _Assets.Scripts.Configs
{
    [CreateAssetMenu(
        fileName = "PrefabsSettings",
        menuName = "MicroFootball/Configs/Prefabs Settings")]
    public class PrefabsSettings : ScriptableObject
    {
        [SerializeField] private BotView _botView;
        [SerializeField] private BallView _ballView;
        
        public BotView BotView => _botView;
        public BallView BallView => _ballView;
    }
}