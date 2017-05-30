using UnityEngine;
using System.Collections;

namespace com.AmazingFusion.HyperWhiteBloodCellDash
{
    public class AudioController : GlobalSingleton<AudioController>
    {
        #region Unity Editor Members

        [Header("Music")]
        [SerializeField]
        AudioClip _battleMusic;
        
        [SerializeField]
        AudioClip _menuMusic;
        

        [Header("Sonido")]
        [SerializeField]
        AudioClip _dashPlayerSound;

        [SerializeField]
        AudioClip _damagePlayerSound;

        [SerializeField]
        AudioClip _deathExplosionPlayerSound;

        [SerializeField]
        AudioClip _shootCannonSound;

        [SerializeField]
        AudioClip _deathEnemySound;

        [SerializeField]
        AudioClip _damageEnemySound;


        #endregion;

        #region Private Members

        bool _isOn;

        AudioSource _musicSource;
        AudioSource _gameFxSource;
        AudioSource _dashFxSource;

        public bool IsOn {
            get {
                return _isOn;
            }

            set {
                _isOn = value;

                _musicSource.volume = _isOn ? 1 : 0;
                _gameFxSource.volume = _isOn ? 1 : 0;
                _dashFxSource.volume = _isOn ? 1 : 0;

                PersistanceManager.Instance.AudioOn = _isOn;
            }
        }

        #endregion

        protected override void Awake()
        {
            base.Awake();

            _musicSource = gameObject.AddComponent<AudioSource>();
            _musicSource.loop = true;
            
            _gameFxSource = gameObject.AddComponent<AudioSource>();
            _dashFxSource = gameObject.AddComponent<AudioSource>();

            //MusicOn(SettingsManager.Instance.MusicOn);
            //SoundOn(SettingsManager.Instance.SoundOn);
        }

        void Start() {
            IsOn = PersistanceManager.Instance.AudioOn;
        }

        public void SwitchAudio() {
            IsOn = !_isOn;
        }

        //public void MusicOn(bool on)
        //{
        //    _musicSource.volume = on ? 1 : 0;
        //}

        //public void SoundOn(bool on)
        //{
        //    _gameFxSource.volume = on ? 1 : 0;
        //}

        public void PlayMusic(AudioClip music)
        {
            _musicSource.Stop();
            _musicSource.clip = music;
            _musicSource.Play();
        }
        

        public void PlayGameSound(AudioClip sound)
        {
            _gameFxSource.Stop();
            _gameFxSource.clip = sound;
            _gameFxSource.Play();
        }

        public void PlayDashSound(AudioClip sound)
        {
            _dashFxSource.Stop();
            _dashFxSource.clip = sound;
            _dashFxSource.Play();
        }

        #region Music

        public void PlayBattleMusic()
        {
            PlayMusic(_battleMusic);
        }

        public void PlayMenuMusic()
        {
            PlayMusic(_menuMusic);
        }

        #endregion
        

        #region Game Sounds

        public void PlayDashPlayerSound()
        {
            PlayDashSound(_dashPlayerSound);
        }

        public void PlayDamagePlayerSound()
        {
            PlayGameSound(_damagePlayerSound);
        }

        public void PlayDeathExplosionPlayerSound()
        {
            PlayGameSound(_deathExplosionPlayerSound);
        }

        public void PlayShootCannonSound()
        {
            PlayGameSound(_shootCannonSound);
        }

        public void PlayDeathEnemySound()
        {
            PlayGameSound(_deathEnemySound);
        }

        public void PlayDamageEnemySound()
        {
            PlayGameSound(_damageEnemySound);
        }

        #endregion
    }
}