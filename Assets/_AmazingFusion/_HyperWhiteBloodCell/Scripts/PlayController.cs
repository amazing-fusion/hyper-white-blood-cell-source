using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class PlayController : OptimizedBehaviour
    {
        void Start()
        {
            AudioController.Instance.PlayMenuMusic();
        }

        public void PlayClick()
        {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.GameScene);
        }

    }
}


