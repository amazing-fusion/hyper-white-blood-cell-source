using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class GameOverView : OptimizedBehaviour
    {
        [SerializeField]
        TMP_Text _deathText;

        [SerializeField]
        TMP_Text _levelText;

        [SerializeField]
        TMP_Text _bestLevelText;

        [SerializeField]
        EasingAnimation _showAnimation;

        [SerializeField]
        EasingAnimation _hideAnimation;

        void Awake()
        {
            GameController.Instance.OnPlayerDied += OnPlayerDie;
            GameController.Instance.OnTimeOver += OnTimeOver;
        }
        
        void Start()
        {
            _hideAnimation.OnEnd += (IEffectable effect) => {
                _hideAnimation.gameObject.SetActive(false);
                GameController.Instance.RestartGame();
            };
        }

        void OnPlayerDie()
        {
            _deathText.text = "You're infected!";
            _showAnimation.gameObject.SetActive(true);
            _showAnimation.Play();
        }

        void OnTimeOver() {
            _deathText.text = "Time's\nup!";
            _showAnimation.gameObject.SetActive(true);
            _showAnimation.Play();
        }

        public void RestartGame()
        {
            _hideAnimation.Play();
        }

        public void GoToMenu()
        {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.MenuScene);
        }
    }
}

