using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class AnimatorControllerPlayer : Singleton<AnimatorControllerPlayer>
    {
        [SerializeField]
        Animator _animatorController;
        
        public void AnimationDiedPlayer()
        {
            _animatorController.SetBool("diedPlayer", true);
        }

        public void AnimationIdlePlayer()
        {
            _animatorController.SetBool("idlePlayer", true);
        }
    }
}

