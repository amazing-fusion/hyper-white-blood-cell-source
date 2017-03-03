using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class PlayController : OptimizedBehaviour
    {
        public void PlayClick()
        {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.GameScene);
        }

    }
}


