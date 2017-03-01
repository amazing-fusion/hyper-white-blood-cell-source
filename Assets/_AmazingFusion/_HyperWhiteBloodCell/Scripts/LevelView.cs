using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class LevelView : OptimizedBehaviour
    {
        [SerializeField]
        TMP_Text _levelText,_highScoreText;
        
        void Inizialize()
        {
            _levelText.text = "Level: " + LevelManager.Instance.CurrentLevel.ToString();
            _highScoreText.text = "HighScore: " + LevelManager.Instance.Highscore.ToString();
        }

        void OnLevelStart(int level)
        {
            _levelText.text = "Level: " +  level.ToString();
        }

    }
}

