using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class GameController : Singleton<GameController>
    {
        void Start()
        {
            EnemyCounter.OnEnemyDestroy += CheckEnemyList;
        }

        void CheckEnemyList()
        {
            if(EnemyCounter._enemies.Count <= 0)
            {
                LevelManager.Instance.CurrentRoom.EndLevel();
            }
        }
    }
}

