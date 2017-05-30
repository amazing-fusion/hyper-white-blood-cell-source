using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCellDash {
    public class PathSpawnOnStart : OptimizedBehaviour {

        [SerializeField]
        PathController _prefab;

        [SerializeField]
        Transform[] _wayPointsPath;

        [SerializeField]
        bool _invertPathOnEnd;

        // Use this for initialization
        void Start() {
            PathController pathController = Instantiate(_prefab,
                    new Vector3(Transform.position.x, Transform.position.y, -3f),
                    Transform.rotation);
            pathController.SetPath(_wayPointsPath, _invertPathOnEnd);
            pathController.Transform.SetParent(Transform.parent, true);
            Destroy(gameObject);
        }
    }
}