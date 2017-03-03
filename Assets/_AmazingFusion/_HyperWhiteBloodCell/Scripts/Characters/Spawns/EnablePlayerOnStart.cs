using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell {
    public class EnablePlayerOnStart : OptimizedBehaviour {

        // Use this for initialization
        void Start() {
            Debug.Log("Spawn player");

            GameController.Instance.Player.Transform.position = Transform.position;
            GameController.Instance.Player.Transform.rotation = Transform.rotation;
            GameController.Instance.Player.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}