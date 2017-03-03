using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.AmazingFusion.HyperWhiteBloodCell
{
    public class GameController : Singleton<GameController>, ITickable
    {
        [SerializeField]
        DamageController _player;

        [SerializeField]
        int _levelTime;

        float _currentLevelTime;

        Transform _playerChild;

        public event System.Action OnPause;

        public event System.Action OnGameRestart;
        public event System.Action OnTimeChange;

        public event System.Action OnPlayerDied;
        public event System.Action OnTimeOver;

        bool _gameIsActive = false;

        public DamageController Player {
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

        public float CurrentLevelTime {
            get {
                return _currentLevelTime;
            }

            set {
                float newTime = Mathf.Max(0, value);
                if (_currentLevelTime != newTime) {
                    _currentLevelTime = newTime;
                    if (OnTimeChange != null) OnTimeChange();
                }
            }
        }

        void Start()
        {
            _player.OnDieEnd += PlayerDied;
            EnemyCounter.OnEnemyDestroy += CheckEnemyList;
            StartGame();
        }

        void OnDestroy() {
            EnemyCounter.OnEnemyDestroy -= CheckEnemyList;

        }

        void EndGame() {
            _gameIsActive = false;
            UpdateManager.Instance.Remove(this);
        }

        void CheckEnemyList()
        {
            if (LevelManager.HasInstance) {
                if (EnemyCounter._enemies.Count <= 0) {
                    LevelManager.Instance.CurrentRoom.EndLevel();
                }
            }
        }

        public void Pause() {
            //TODO: Fix the hack
            Time.timeScale = 0;
            if (OnPause != null) OnPause();
        }

        public void Resume() {
            Time.timeScale = 1;
        }

        public void StartGame() {
            _gameIsActive = true;
            _player.Initialize();
            LevelManager.Instance.FirstLevel();
            CurrentLevelTime = _levelTime;

            LevelManager.Instance.CurrentRoom.StartLevel();

            UpdateManager.Instance.Add(this);

            AudioController.Instance.PlayBattleMusic();
        }

        public void RestartGame() {
            if (OnGameRestart != null) OnGameRestart();
            StartGame();
        }

        void PlayerDied() {
            if (_gameIsActive) {
                EndGame();
                if (OnPlayerDied != null) OnPlayerDied();
            }
            else {
                if (OnTimeOver != null) OnTimeOver();
            }
        }

        void TimeOver() {
            if (_gameIsActive) {
                EndGame();
                _player.Die();
            }
        }

        public void Tick(float realDeltaTime) {
            CurrentLevelTime -= Time.deltaTime;
            if (CurrentLevelTime <= 0) {
                TimeOver();
            }
        }
    }
}