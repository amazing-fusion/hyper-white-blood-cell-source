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

        public static event System.Action<Room> OnLevelStart;
        public static event System.Action<Room> OnLevelEnd;

        public void StartLevel()
        {
            if (OnLevelStart != null) OnLevelStart(this);
        }

        public void EndLevel()
        {
            if (OnLevelEnd != null) OnLevelEnd(this);
        }
    }
}

