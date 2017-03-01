using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class GameController : Singleton<GameController>
    {
        [SerializeField]
        SwipeInputController _player;

        public SwipeInputController Player {
            get {
                return _player;
            }
        }

        void Start()
        {
            EnemyCounter.OnEnemyDestroy += CheckEnemyList;
        }

        void CheckEnemyList()
        {
            if (LevelManager.HasInstance) {
                if (EnemyCounter._enemies.Count <= 0) {
                    LevelManager.Instance.CurrentRoom.EndLevel();
                }
            }
        }
    }
}

