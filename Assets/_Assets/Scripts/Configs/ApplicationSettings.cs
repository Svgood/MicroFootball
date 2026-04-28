using UnityEngine;

namespace MicroFootball.Configs
{
    [CreateAssetMenu(
        fileName = "ApplicationSettings",
        menuName = "MicroFootball/Configs/Application Settings")]
    public sealed class ApplicationSettings : ScriptableObject
    {
        [SerializeField] private string _menuSceneName = "Menu";
        [SerializeField] private string _gameplaySceneName = "Gameplay";

        public string MenuSceneName => _menuSceneName;
        public string GameplaySceneName => _gameplaySceneName;
    }
}
