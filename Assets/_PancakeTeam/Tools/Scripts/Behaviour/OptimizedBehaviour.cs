using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.PancakeTeam {
    public class OptimizedBehaviour : MonoBehaviour {

        Transform _transform = null;
        public Transform Transform {
            get {
                if (_transform == null) {
                    _transform = transform;
                }
                return _transform;
            }
        }
    }
}
