using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace com.AmazingFusion
{
    [RequireComponent(typeof(Slider))]
    public class LoadingProgressBar : OptimizedBehaviour
    {
        Slider _slider;
        void Start()
        {
            _slider = GetComponent<Slider>();
        }

        void OnGUI()
        {
            _slider.value = SceneLoader.Progress;
        }
    }
}