using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCellDash {
    public class AnimatorControllerPlayer : Singleton<AnimatorControllerPlayer>
    {
        [SerializeField]
        Animator _animatorController;
        
        public void AnimationDiedPlayer()
        {
            _animatorController.SetInteger("statePlayer", 1);
        }

        public void AnimationIdlePlayer()
        {
            _animatorController.SetInteger("statePlayer", 0);
        }

        public void AnimationAtkPlayer()
        {
            _animatorController.SetInteger("statePlayer", 2);
        }
    }
}

