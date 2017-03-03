using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class GameController : Singleton<GameController>
    {
        [SerializeField]
        SwipeInputController _player;

        Transform _playerChild;

        public event System.Action OnGameRestart;

        public SwipeInputController Player {
            get {
                return _player;
            }
        }

        public Transform PlayerChild
        {
            get
            {
                return _player.Transform.GetChild(0);
            }

            
        }

        void Start()
        {
            EnemyCounter.OnEnemyDestroy += CheckEnemyList;
            StartGame();
        }

        void OnDestroy() {
            EnemyCounter.OnEnemyDestroy -= CheckEnemyList;
        }

        void CheckEnemyList()
        {
            if (LevelManager.HasInstance) {
                if (EnemyCounter._enemies.Count <= 0) {
                    LevelManager.Instance.CurrentRoom.EndLevel();
                }
            }
        }

        public void StartGame() {
            _player.GetComponent<DamageController>().Initialize();
            LevelManager.Instance.FirstLevel();
        }

        public void RestartGame() {
            if (OnGameRestart != null) OnGameRestart();
            StartGame();
        }
    }
}

