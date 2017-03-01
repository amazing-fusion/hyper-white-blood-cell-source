using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class GameOverView : OptimizedBehaviour
    {
        [SerializeField]
        DamageController _player;

        [SerializeField]
        TMP_Text _highScoreText;

        [SerializeField]
        Transform _gameOverTransform;

        void Awake()
        {
            _player.OnDieEnd += OnPlayerDie;
        }
        
        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            _gameOverTransform.gameObject.SetActive(false);
        }

        void OnPlayerDie()
        {
            _gameOverTransform.gameObject.SetActive(true);
        }

        public void RestartGame()
        {

        }

        public void GoToMenu()
        {

        }
    }
}

