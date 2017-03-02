using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class AnimatorControllerEnemy : Singleton<AnimatorControllerEnemy>
    {
        [SerializeField]
        Animator _animatorController;

        [SerializeField]
        SpriteRenderer _spriteExplosionController;

        void Start()
        {
            _animatorController.enabled = false;
            _spriteExplosionController.enabled = false;
        }

        public void AnimationDiedEnemy()
        {
            _animatorController.enabled = true;
            _spriteExplosionController.enabled = true;
        }
    }
}

