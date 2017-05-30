using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementEffects;
using TMPro;
using System;

namespace com.AmazingFusion.HyperWhiteBloodCellDash {
    [RequireComponent(typeof(TMP_Text))]
    public class ChangeTextAnimation : OptimizedBehaviour, IFixedTickable {

        [SerializeField]
        string[] _texts;

        [SerializeField]
        float _rate;

        TMP_Text _text;

        float _nextChange;
        int _currentIndex;

        void OnDestroy() {
            if (FixedUpdateManager.HasInstance) {
                FixedUpdateManager.Instance.Remove(this);
            }
        }

        void Awake() {
            _text = GetComponent<TMP_Text>();

            FixedUpdateManager.Instance.Add(this);
        }

        public void FixedTick(float realDeltaTime) {
            if (Time.time > _nextChange) {
                ++_currentIndex;
                if (_currentIndex >= _texts.Length) {
                    _currentIndex = 0;
                }

                _text.text = _texts[_currentIndex];

                _nextChange = Time.time + _rate;
            }
        }
    }
}
