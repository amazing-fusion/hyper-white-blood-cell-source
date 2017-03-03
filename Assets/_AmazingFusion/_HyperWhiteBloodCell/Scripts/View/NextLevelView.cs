using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class NextLevelView : OptimizedBehaviour
    {
        EasingAnimation _showAnimation;

        [SerializeField]
        Image _fadeImage;

        void Awake()
        {
            Room.OnLevelEnd += OnLevelEnd;
        }

        void OnLevelEnd(Room room)
        {
            Show();
            LevelManager.Instance.NextLevel();
            Hide();
            LevelManager.Instance.CurrentRoom.StartLevel();
        }

        void Show()
        {
            _fadeImage.enabled = true;
        }

        void Hide()
        {
            _fadeImage.enabled = false;
        }
    }
}

