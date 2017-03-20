using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    [RequireComponent(typeof(DamageController))]
    public class EnemyCounter : OptimizedBehaviour
    {
        public static List<EnemyCounter> _enemies = new List<EnemyCounter>();
        public static event System.Action OnEnemyDestroy;

        DamageController _damageController;

        void OnDestroy() {
            _damageController.OnDie -= OnEnemyDie;
        }

        void OnEnable()
        {
            _enemies.Add(this);
            _damageController = GetComponent<DamageController>();
            _damageController.OnDie += OnEnemyDie;
        }

        void OnDisable()
        {
            _damageController.OnDie -= OnEnemyDie;
            Remove(this);
        }

        void OnEnemyDie(System.Action onDieEnd) {
            _damageController.OnDie -= OnEnemyDie;
            Remove(this);
        }

        static void Remove(EnemyCounter enemy) {
            if (_enemies.Contains(enemy)) {
                _enemies.Remove(enemy);
                if (OnEnemyDestroy != null) OnEnemyDestroy();
            }
        }

    }
}
