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

        //DamageController _damageController;

        void Start()
        {
            _enemies.Add(this);
            //_damageController = GetComponent<DamageController>();
            //_damageController.OnDie += (System.Action onDieEnd) => {
            //    Remove(this);
            //};
        }

        void OnDisable()
        {
            //if (_enemies.Contains(this)) {
                _enemies.Remove(this);
            //}
            Debug.Log("Enemies: " + _enemies.Count);
            if (OnEnemyDestroy != null) OnEnemyDestroy();
        }

        //static void Remove(EnemyCounter enemy) {
        //    _enemies.Remove(enemy);
        //}

    }
}
