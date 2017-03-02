using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion {
    public class OrtographicCameraResizer : MonoBehaviour {

        [SerializeField]
        float _referenceWidth;

        [SerializeField]
        float _referenceHeight;

        [SerializeField]
        float _match;

        // Use this for initialization
        void Start() {
            SetCamera();

#if !UNITY_EDITOR
                Destroy(this);
#endif

        }

#if UNITY_EDITOR
        void Update() {
            SetCamera();
        }
#endif

        void SetCamera() {
            float ratio = (_referenceWidth / _referenceHeight) / Camera.main.aspect;
            Camera.main.orthographicSize = _referenceHeight * (ratio + (1 - ratio) * _match);
        }
    }
}