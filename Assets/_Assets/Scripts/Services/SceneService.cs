using _Assets.Scripts.Configs;
using UnityEngine.SceneManagement;

namespace _Assets.Scripts.Services
{
    public interface ISceneService
    {
        void LoadMenu();
        void LoadGameplay();
    }
    
    public sealed class SceneService : ISceneService
    {
        private readonly ApplicationSettings _settings;

        public SceneService(ApplicationSettings settings)
        {
            _settings = settings;
            
            LoadMenu();
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene(_settings.MenuSceneName);
        }

        public void LoadGameplay()
        {
            SceneManager.LoadScene(_settings.GameplaySceneName);
        }
    }
}
