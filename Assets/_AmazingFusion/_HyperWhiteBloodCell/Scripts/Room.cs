using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class Room : OptimizedBehaviour
    {
        [SerializeField]
        int _minLevelRequired;

        [SerializeField]
        int _maxLevelRequired;

        public int MinLevelRequired
        {
            get
            {
                return _minLevelRequired;
            }

            set
            {
                _minLevelRequired = value;
            }
        }

        public int MaxLevelRequired
        {
            get
            {
                return _maxLevelRequired;
            }

            set
            {
                _maxLevelRequired = value;
            }
        }

        public event System.Action<int> OnLevelStart;

        public void StartLevel()
        {

        }

        public void EndLevel()
        {

        }
    }
}

