using System.Collections;
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
        Room _currentRoom;
        Room _lastRoom;

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

        public Room LastRoom
        {
            get
            {
                return _lastRoom;
            }

            set
            {
                _lastRoom = value;
            }
        }

        void LoadLevel()
        {
            Room temRoom;
            List<Room> temListRoom = new List<Room>();
            foreach(Room roomPrefab in _roomsPrefab)
            {
                if(CurrentLevelNumber >= roomPrefab.MinLevelRequired && 
                        (CurrentLevelNumber <= roomPrefab.MaxLevelRequired ||
                        roomPrefab.MaxLevelRequired == 0) && roomPrefab != _lastRoom)
                {
                    temListRoom.Add(roomPrefab);
                }
            }
            temRoom = temListRoom[Random.Range(0, temListRoom.Count)];
            _currentRoom = Instantiate(temRoom);
            _lastRoom = temRoom;
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

