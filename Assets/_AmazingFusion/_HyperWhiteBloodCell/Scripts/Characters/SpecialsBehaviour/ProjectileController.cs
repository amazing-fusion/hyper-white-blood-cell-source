using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class ProjectileController : OptimizedBehaviour {

        [SerializeField]
        float _speed;

        Rigidbody2D _rigidBody;

        void Start() {
            _rigidBody = GetComponent<Rigidbody2D>();
            _rigidBody.velocity = transform.up * _speed;
        }

        void OnTriggerEnter2D(Collider2D collider) {
            Explode();
        }

        void Explode() {
            //TODO: Make awesome effect
            _rigidBody.velocity = Vector2.zero;
            Destroy(gameObject);
        }
    }
}