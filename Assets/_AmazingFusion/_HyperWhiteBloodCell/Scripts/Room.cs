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

        bool _started;

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

        public bool Started {
            get {
                return _started;
            }
        }

        public static event System.Action<Room> OnLevelStart;
        public static event System.Action<Room> OnLevelEnd;

        public void StartLevel()
        {
            _started = true;
            if (OnLevelStart != null) OnLevelStart(this);
        }

        public void EndLevel()
        {
            _started = false;
            if (OnLevelEnd != null) OnLevelEnd(this);
        }
    }
}

