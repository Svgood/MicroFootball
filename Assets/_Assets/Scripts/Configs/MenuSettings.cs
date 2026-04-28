using UnityEngine;

namespace MicroFootball.Configs
{
    [CreateAssetMenu(
        fileName = "MenuSettings",
        menuName = "MicroFootball/Configs/Menu Settings")]
    public sealed class MenuSettings : ScriptableObject
    {
        [SerializeField] private bool _autostart = false;
        [SerializeField] private float _autostartDelaySeconds = 0.5f;

        public bool Autostart => _autostart;
        public float AutostartDelaySeconds => _autostartDelaySeconds;
    }
}
