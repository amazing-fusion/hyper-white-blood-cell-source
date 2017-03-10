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

        [SerializeField]
        string _yesText;

        [SerializeField]
        string _noText;

        MobileNativeDialog _dialogPopUp = null;

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
            _dialogPopUp.OnComplete -= OnDialogClose;
            _dialogPopUp = null;
            //parsing result
            if(result == MNDialogResult.YES)
            {
                Application.Quit();
            }
        }

        public void Tick(float realDeltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_dialogPopUp == null)
                {
                    _dialogPopUp = new MobileNativeDialog(_titlePopUp, _dialogMessagePopUp,_yesText,_noText);
                    _dialogPopUp.OnComplete += OnDialogClose;
                }
                
            }
        }
    }
}

