using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class EnemyCounter : OptimizedBehaviour
    {
        public static List<EnemyCounter> _enemies = new List<EnemyCounter>();
        public static event System.Action OnEnemyDestroy;

        void Start()
        {
            _enemies.Add(this);
            Debug.Log("Enemies: " + _enemies.Count);
        }

        void OnDisable()
        {
            _enemies.Remove(this);
            Debug.Log("Enemies: " + _enemies.Count);
            if (OnEnemyDestroy != null) OnEnemyDestroy();
        }

    }
}
