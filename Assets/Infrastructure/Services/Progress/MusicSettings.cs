using System;

namespace Infrastructure.Services.Progress
{
    [Serializable]
    public class MusicSettings
    {
        public Action OnMusicSettingsChanged { get; set; }

        public bool MusicMuted {
            get => _musicMuted;
            set
            {
                _musicMuted = value;
                OnMusicSettingsChanged?.Invoke();
            }
        }
        
        public bool SoundsMuted {
            get => _soundsMuted;
            set
            {
                _soundsMuted = value;
                OnMusicSettingsChanged?.Invoke();
            }
        }
        
        public float MusicVolume {
            get => _musicVolume;
            set
            {
                _musicVolume = value;
                OnMusicSettingsChanged?.Invoke();
            }
        }
        
        public float SoundsVolume {
            get => _soundsVolume;
            set
            {
                _soundsVolume = value;
                OnMusicSettingsChanged?.Invoke();
            }
        }

        private bool _musicMuted;
        private bool _soundsMuted;
        private float _musicVolume;
        private float _soundsVolume;

        public MusicSettings()
        {
            _musicMuted = true;
            _soundsMuted = true;
            _musicVolume = 1f;
            _soundsVolume = 1f;
        }
    }
}