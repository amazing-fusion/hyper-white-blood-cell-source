using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class ShooterController : OptimizedBehaviour, ITickable {

        [SerializeField]
        ProjectileController _projectilePrefab;

        [SerializeField]
        float _fireSleep;

        float _nextShotTime;

        void OnDestroy() {
            if (UpdateManager.HasInstance) {
                UpdateManager.Instance.Remove(this);
            }
        }

        void Start() {
            _nextShotTime = Time.time + _nextShotTime;

            UpdateManager.Instance.Add(this);
        }

        public void Tick(float realDeltaTime) {
            if (Time.time >= _nextShotTime) {
                Shot();
            }
        }

        void Shot() {
            Vector2 position = Transform.position + Transform.forward * 0.5f;
            Instantiate(_projectilePrefab, position, Transform.rotation);
            _nextShotTime = Time.time + _fireSleep;
        }
    }
}