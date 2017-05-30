using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace com.AmazingFusion.HyperWhiteBloodCellDash
{
    public class PopUpController : OptimizedBehaviour, ITickable
    {
        [System.Serializable]
        public class VoidEvent : UnityEvent { };

        [SerializeField]
        string _titlePopUp;

        [SerializeField]
        string _dialogMessagePopUp;

        [SerializeField]
        VoidEvent _onAcceptDialog;

        MobileNativeDialog _dialogPopUp;

        void Awake()
        {
            UpdateManager.Instance.Add(this);
            
        }

        void OnDestroy()
        {
            if (UpdateManager.HasInstance)
            {
                UpdateManager.Instance.Remove(this);
            }
        }

        private void OnDialogClose(MNDialogResult result)
        {
            //parsing result
            switch (result)
            {
                case MNDialogResult.YES:
                    _onAcceptDialog.Invoke();
                    break;
                case MNDialogResult.NO:

                    break;
            }
        }

        public void Tick(float realDeltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _dialogPopUp = new MobileNativeDialog(_titlePopUp, _dialogMessagePopUp);
                _dialogPopUp.OnComplete += OnDialogClose;
            }
        }

        public void QuitGame() {
            NotificationsController.Instance.SetNotifications();
            Application.Quit();
        }

        public void GoToMenuScene() {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.MenuScene);
        }
    }
}

