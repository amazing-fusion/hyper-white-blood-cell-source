using UnityEngine;
using System.Collections;

namespace com.PancakeTeam
{
    public class GlobalSingleton<T> : OptimizedBehaviour where T : OptimizedBehaviour {
        static bool _destroyed = false;

        static T _instance = null;
        public static T Instance
        {
            get
            {
                CreateInstance();

                return _instance;
            }
        }

        public static bool HasInstance
        {
            
            get { Debug.Log(_instance);  return _instance != null; }
        }

        static void CreateInstance()
        {
            if (!_destroyed && _instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();
                }

                DontDestroyOnLoad(_instance.gameObject);
            }
        }

        virtual protected void Awake()
        {
            CreateInstance();
        }

        void OnApplicationQuit()
        {
            _instance = null;
            _destroyed = true;
        }
    }
}