using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace com.AmazingFusion.HyperWhiteBloodCellDash {
    [RequireComponent(typeof(TMP_Text))]
    public class RandomText : OptimizedBehaviour {

        [SerializeField]
        string[] _randomText;

        void Awake() {
            GetComponent<TMP_Text>().text = _randomText[Random.Range(0, _randomText.Length)];
        }
    }
}
