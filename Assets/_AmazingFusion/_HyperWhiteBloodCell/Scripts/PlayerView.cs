using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class PlayerView : OptimizedBehaviour
    {
        [SerializeField]
        DamageController _player;

        [SerializeField]
        EasingAnimation[] _lifesAnimation;
        
        void Awake()
        {
            _player.OnLifesChange += OnLifesChange;
        }

        void Initialize()
        {
            foreach(EasingAnimation life in _lifesAnimation)
            {
                life.gameObject.SetActive(true);
            }
        }

        void OnLifesChange()
        {
            foreach(EasingAnimation lifes in _lifesAnimation)
            {
                if (lifes.enabled)
                {
                    lifes.Play();
                    break;
                }
            }
        }
    }
}

