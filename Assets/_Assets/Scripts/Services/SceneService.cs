using MicroFootball.Configs;
using UnityEngine.SceneManagement;

namespace MicroFootball.Application.Services
{
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
