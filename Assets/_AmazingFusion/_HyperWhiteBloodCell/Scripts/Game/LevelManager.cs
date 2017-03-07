﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField]
        Room[] _roomsPrefab;

        [SerializeField]
        float _startLevelDelay;

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

        Room _currentRoom;

        public Room CurrentRoom
        {
            get
            {
                return _currentRoom;
            }
        }

        public float StartLevelDelay {
            get {
                return _startLevelDelay;
            }
        }

        void LoadLevel()
        {
            List<Room> temListRoom = new List<Room>();
            foreach(Room roomPrefab in _roomsPrefab)
            {
                if(CurrentLevelNumber >= roomPrefab.MinLevelRequired && 
                        (CurrentLevelNumber <= roomPrefab.MaxLevelRequired ||
                        roomPrefab.MaxLevelRequired == 0))
                {
                    temListRoom.Add(roomPrefab);
                }
            }
            _currentRoom = Instantiate(temListRoom[Random.Range(0, temListRoom.Count)]);
        }

        public void FirstLevel()
        {
            if(CurrentRoom != null)
            {
                Destroy(CurrentRoom.gameObject);
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
            PersistanceManager.Instance.BestLevel = CurrentLevelNumber;
            LoadLevel();
        }
    }
}

