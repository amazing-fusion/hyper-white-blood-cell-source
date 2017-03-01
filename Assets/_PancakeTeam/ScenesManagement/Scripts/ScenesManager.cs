using UnityEngine;
using System.Collections;

namespace com.PancakeTeam
{
    public class ScenesManager : GlobalSingleton<ScenesManager>
    {
        public enum Scene
        {
            LoadingScene,
            MenuScene,
            GameScene
        }

        Scene _currentScene = Scene.MenuScene;
        public Scene CurrentScene
        {
            get { return _currentScene; }
            set { _currentScene = value; }
        }

        public void LoadScene(Scene scene)
        {
            if (scene == Scene.LoadingScene) {
                Debug.LogWarning("[SceneManager] Some script is attempting to load the Loading Scene");
                return;
            }

            //AnalyticsManager.LoadSceneEvent(_currentScene.ToString());

            _currentScene = scene;
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)Scene.LoadingScene);
        }
    }
}