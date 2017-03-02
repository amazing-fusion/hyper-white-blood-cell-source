﻿using System.Collections;
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

        void Awake()
        {
            LevelManager.Instance.OnLevelChange += OnLevelChange;
        }
        
        void Inizialize()
        {
            _levelText.text = (LevelManager.Instance.CurrentLevelNumber + 1).ToString();
            _highScoreText.text = string.Format("BEST: {0}", 0);
        }

        void OnLevelChange()
        {
            _levelText.text = "Level: " + (LevelManager.Instance.CurrentLevelNumber + 1).ToString();
        }

    }
}

