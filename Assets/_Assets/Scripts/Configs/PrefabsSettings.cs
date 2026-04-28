using MicroFootball.Gameplay.View;
using UnityEngine;

namespace MicroFootball.Configs
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