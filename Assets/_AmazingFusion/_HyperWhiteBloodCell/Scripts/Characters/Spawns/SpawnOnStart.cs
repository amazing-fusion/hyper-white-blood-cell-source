using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class SpawnOnStart : OptimizedBehaviour {

        [SerializeField]
        Transform _prefab;

        // Use this for initialization
        void Start() {
            Transform go = Instantiate(_prefab, Transform.position, Transform.rotation);
            go.SetParent(Transform.parent, true);
            Destroy(gameObject);
        }
    }
}