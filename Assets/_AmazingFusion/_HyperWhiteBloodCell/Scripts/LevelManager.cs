using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField]
        Room[] _roomsPrefab;

        int _currentLevelNumber;
        int _highscore;

        public int CurrentLevelNumber
        {
            get
            {
                return _currentLevelNumber;
            }

            set
            {
                if(_currentLevelNumber != value)
                {
                    _currentLevelNumber = value;
                    if (OnLevelChange != null) OnLevelChange();
                }
            }
        }

        public event System.Action OnLevelChange;

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

        void Start()
        {
            FirstLevel();
        }

        Room _currentRoom;

        public Room CurrentRoom
        {
            get
            {
                return _currentRoom;
            }

            set
            {
                _currentRoom = value;
            }
        }

        void LoadLevel()
        {
            List<Room> temListRoom = new List<Room>();
            foreach(Room a in _roomsPrefab)
            {
                if(CurrentLevelNumber >= a.MinLevelRequired && (CurrentLevelNumber <= a.MaxLevelRequired ||
                    a.MaxLevelRequired == 0))
                {
                    temListRoom.Add(a);
                }
            }
            _currentRoom = Instantiate(temListRoom[Random.Range(0,temListRoom.Count)]);
        }

        public void FirstLevel()
        {
            if(CurrentRoom != null)
            {
                Destroy( CurrentRoom.gameObject);
            }
            CurrentLevelNumber = 0;
            LoadLevel();
        }

        public void NextLevel()
        {
            if (CurrentRoom != null)
            {
                Destroy(CurrentRoom.gameObject);
            }
            CurrentLevelNumber++;
            LoadLevel();
        }
    }
}

