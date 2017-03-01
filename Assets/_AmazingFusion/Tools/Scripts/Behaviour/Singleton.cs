using UnityEngine;
using System.Collections;

namespace com.AmazingFusion
{
    public class Singleton<T> : OptimizedBehaviour where T : OptimizedBehaviour {
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

        virtual protected void Awake()
        {
            CreateInstance();
        }

        public static bool HasInstance
        {
            get { return _instance != null; }
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
            }
        }

        void OnApplicationQuit()
        {
            _instance = null;
            _destroyed = true;
        }
    }
}