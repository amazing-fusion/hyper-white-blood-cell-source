using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class ProjectileController : OptimizedBehaviour {

        [SerializeField]
        float _speed;

        [SerializeField]
        List<string> _explodeTags;

        Rigidbody2D _rigidBody;

        EZObjectPool _projectilesPool;

        void Awake() {
            _rigidBody = GetComponent<Rigidbody2D>();
            
        }

        void OnEnable() {
            Room.OnLevelEnd += LevelEnd;
        }

        void OnDisable() {
            Room.OnLevelEnd -= LevelEnd;
        }

        void OnDestroy() {
            Room.OnLevelEnd -= LevelEnd;
        }

        void LevelEnd(Room room, bool win) {
            Delete();
        }

        void OnTriggerEnter2D(Collider2D collider) {
            if (_explodeTags.Contains(collider.tag)) {
                Explode();
            }
        }

        void OnBecameInvisible() {
            Delete();
        }

        void Explode() {
            //TODO: Make awesome effect
            _rigidBody.velocity = Vector2.zero;

            Delete();
        }

        void Delete() {
            gameObject.SetActive(false);
            _projectilesPool.AddToAvailableObjects(gameObject);
        }

        public void Initialize(EZObjectPool projectilesPool) {
            _projectilesPool = projectilesPool;
            _rigidBody.velocity = transform.up * _speed;
        }
    }
}