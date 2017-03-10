using UnityEngine;
using System.Collections;

namespace com.AmazingFusion
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
                return;
            }

            //AnalyticsManager.LoadSceneEvent(_currentScene.ToString());

            _currentScene = scene;
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)Scene.LoadingScene);
        }
    }
}