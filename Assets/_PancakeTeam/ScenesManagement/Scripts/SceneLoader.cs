using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using MovementEffects;
using System.Collections.Generic;

namespace com.PancakeTeam
{
    public class SceneLoader : OptimizedBehaviour
    {
        LoadSceneMode _loadSceneMode = LoadSceneMode.Single;

        static AsyncOperation _loadOperation = null;

        public LoadSceneMode LoadSceneMode
        {
            get
            {
                return _loadSceneMode;
            }

            set
            {
                _loadSceneMode = value;
            }
        }

        public static float Progress
        {
            get { return _loadOperation == null ? 0 : _loadOperation.progress; }
        }

        public static bool IsDone
        {
            get { return _loadOperation == null ? false : _loadOperation.isDone; }
        }

        void Start()
        {
            if (ScenesManager.Instance.CurrentScene != ScenesManager.Scene.LoadingScene)
            {
                Timing.RunCoroutine(Load());
            }
        }

        private IEnumerator<float> Load()
        {
            _loadOperation = SceneManager.LoadSceneAsync((int)ScenesManager.Instance.CurrentScene, _loadSceneMode);
            _loadOperation.allowSceneActivation = _loadSceneMode == LoadSceneMode.Single;
            yield return 0f;
        }

        public IEnumerator<float> ActivateScene()
        {
            _loadOperation.allowSceneActivation = true;

            while (!_loadOperation.isDone)
            {
                yield return 0f;
            }
            
            SceneManager.UnloadSceneAsync((int)ScenesManager.Scene.MenuScene);
        }
    }
}