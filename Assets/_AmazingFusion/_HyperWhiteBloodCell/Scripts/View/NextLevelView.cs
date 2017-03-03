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
            _showAnimation.OnEnd += OnShowAnimationEnd;
            _hideAnimation.OnEnd += OnHideAnimationEnd;

            Room.OnLevelEnd += OnLevelEnd;
        }

        void OnDestroy() {
            _showAnimation.OnEnd -= OnShowAnimationEnd;
            _hideAnimation.OnEnd -= OnHideAnimationEnd;
        }

        void OnShowAnimationEnd(IEffectable effect) {
            LevelManager.Instance.NextLevel();
            Hide();
        }

        void OnHideAnimationEnd(IEffectable effect) {
            Timing.RunCoroutine(DoNextLevel());
        }

        void OnLevelEnd(Room room)
        {
            Show();
        }

        void Show()
        {
            if (_showAnimation != null) {
                _showAnimation.gameObject.SetActive(true);
                _showAnimation.Play();
            }
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

