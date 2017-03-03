using MovementEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class NextLevelView : OptimizedBehaviour
    {
        [SerializeField]
        EasingAnimation _showAnimation;

        [SerializeField]
        EasingAnimation _hideAnimation;

        [SerializeField]
        Image _fadeImage;

        void Awake()
        {
            _showAnimation.OnEnd += (IEffectable effect) => {
                LevelManager.Instance.NextLevel();
                Hide();
            };
            _hideAnimation.OnEnd += (IEffectable effect) => {
                Timing.RunCoroutine(DoNextLevel());
            };

            Room.OnLevelEnd += OnLevelEnd;
        }

        void OnLevelEnd(Room room)
        {
            Show();
        }

        void Show()
        {
            _showAnimation.gameObject.SetActive(true);
            _showAnimation.Play();
        }

        void Hide()
        {
            _hideAnimation.Play();
        }

        IEnumerator<float> DoNextLevel() {
            _hideAnimation.gameObject.SetActive(false);
            yield return Timing.WaitForSeconds(0.5f);
            LevelManager.Instance.CurrentRoom.StartLevel();
        }
    }
}

