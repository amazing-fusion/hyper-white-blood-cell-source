using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCellDash {
    public class SpawnOnStart : OptimizedBehaviour {

        [SerializeField]
        Transform _prefab;

        // Use this for initialization
        void Start() {
            Transform go = Instantiate(_prefab,
                    new Vector3(Transform.position.x, Transform.position.y, -3f),
                    Transform.rotation);
            go.SetParent(Transform.parent, true);
            Destroy(gameObject);
        }
    }
}