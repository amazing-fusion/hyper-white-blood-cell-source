using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField]
        Room[] _rooms;

        int _currentLevel;
        int _highscore;

        public int CurrentLevel
        {
            get
            {
                return _currentLevel;
            }

            set
            {
                _currentLevel = value;
            }
        }

        public int Highscore
        {
            get
            {
                return _highscore;
            }

            set
            {
                _highscore = value;
            }
        }

        public void FirstLevel()
        {
            CurrentLevel = 0;
        }

        public void NextLevel()
        {
            CurrentLevel++;
        }
    }
}

