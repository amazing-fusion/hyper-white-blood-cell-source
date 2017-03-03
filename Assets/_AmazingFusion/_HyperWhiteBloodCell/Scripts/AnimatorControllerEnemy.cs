using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class AnimatorControllerEnemy : Singleton<AnimatorControllerEnemy>
    {
        Animator _animatorController;
        SpriteRenderer _spriteExplosionController;

        void Start()
        {
            _animatorController = Transform.GetChild(0).GetChild(0).GetComponent<Animator>();
            _spriteExplosionController = Transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();

            _animatorController.enabled = false;
            _spriteExplosionController.enabled = false;
        }

        public void AnimationDiedEnemy()
        {
            Debug.Log("Animacion Muerte de " 
                 + _animatorController.transform.parent.parent.gameObject);
            _animatorController.enabled = true;
            _spriteExplosionController.enabled = true;
            _animatorController.Play(0);
        }
    }
}

