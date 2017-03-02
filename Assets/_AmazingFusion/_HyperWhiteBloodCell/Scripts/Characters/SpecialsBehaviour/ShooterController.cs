using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class ShooterController : OptimizedBehaviour, ITickable {

        [SerializeField]
        float _fireSleep;

        [Header("Pool")]
        [SerializeField]
        ProjectileController _projectilePrefab;

        [SerializeField, Tooltip("How many objects will be in the scene?")]
        int _poolSize;

        EZObjectPool _projectilesPool;

        float _nextShotTime;

        void OnDestroy() {
            if (UpdateManager.HasInstance) {
                UpdateManager.Instance.Remove(this);
            }
        }

        void Awake() {
            _projectilesPool = EZObjectPool.CreateObjectPool(_projectilePrefab.gameObject, string.Format("{0}_ProjectilesPool", name), _poolSize, true, true, false);
        }

        void Start() {
            _nextShotTime = Time.time + _fireSleep;

            UpdateManager.Instance.Add(this);
        }

        public void Tick(float realDeltaTime) {
            if (Time.time >= _nextShotTime) {
                Shot();
            }
        }

        void Shot() {
            Vector2 position = Transform.position + Transform.up * 0.5f;

            GameObject projectile;
            if (_projectilesPool.TryGetNextObject(position, Transform.rotation, out projectile)) {
                projectile.GetComponent<ProjectileController>().Initialize(_projectilesPool);
                _nextShotTime = Time.time + _fireSleep;
            }
        }
    }
}