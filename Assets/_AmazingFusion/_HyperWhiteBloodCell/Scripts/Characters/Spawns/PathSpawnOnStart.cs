using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class PathSpawnOnStart : OptimizedBehaviour {

        [SerializeField]
        PathController _prefab;

        [SerializeField]
        Transform[] _wayPointsPath;

        [SerializeField]
        bool _invertPathOnEnd;

        // Use this for initialization
        void Start() {
            PathController pathController = Instantiate(_prefab, Transform.position, Transform.rotation);
            pathController.Initialize(_wayPointsPath, _invertPathOnEnd);
            pathController.Transform.SetParent(Transform.parent, true);
            Destroy(gameObject);
        }
    }
}