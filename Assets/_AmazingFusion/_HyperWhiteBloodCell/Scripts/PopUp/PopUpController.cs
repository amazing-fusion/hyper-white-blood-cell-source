using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class PopUpController : OptimizedBehaviour, ITickable
    {
        [SerializeField]
        string _titlePopUp;

        [SerializeField]
        string _dialogMessagePopUp;

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
                    Application.Quit();
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
    }
}

