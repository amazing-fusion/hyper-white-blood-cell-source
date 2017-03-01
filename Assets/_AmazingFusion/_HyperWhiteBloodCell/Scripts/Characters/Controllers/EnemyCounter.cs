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
        }

        void OnDestroy()
        {
            _enemies.Remove(this);
            if (OnEnemyDestroy != null) OnEnemyDestroy();
        }

    }
}
