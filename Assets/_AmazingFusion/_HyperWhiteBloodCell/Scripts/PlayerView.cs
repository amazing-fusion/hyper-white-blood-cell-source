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
        Image _life0_On;

        [SerializeField]
        Image _life0_Off;

        [SerializeField]
        Image _life1_On;

        [SerializeField]
        Image _life1_Off;

        [SerializeField]
        Image _life2_On;

        [SerializeField]
        Image _life2_Off;

        [SerializeField]
        EasingAnimation[] _lifesAnimation;
        
        void Awake()
        {
            Initialize();
            _player.OnLifesChange += OnLifesChange;
        }

        void Initialize()
        {
            _life0_On.color = Color.white;
            _life1_On.color = Color.white;
            _life2_On.color = Color.white;

            _life0_Off.color = new Color(1f, 1f, 1f, 0);
            _life1_Off.color = new Color(1f, 1f, 1f, 0);
            _life2_Off.color = new Color(1f, 1f, 1f, 0);
        }

        void OnLifesChange(int lifes)
        {
            if (lifes < 0) return;
            
            if (lifes < _lifesAnimation.Length) {
                _lifesAnimation[lifes].Play();
            }
        }
    }
}

