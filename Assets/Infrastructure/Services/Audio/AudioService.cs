using Infrastructure.Services.Factory;
using Infrastructure.Services.Progress;
using Infrastructure.Services.StaticData;

namespace Infrastructure.Services.Audio
{
    public class AudioService : IAudioService
    {
        private const int MuteDb = 50;
        private const string CacheSoundsVolume = "SoundsVolume";
        private const string CacheMusicVolume = "MusicVolume";

        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IPersistentProgress _progress;

        public AudioService(IGameFactory gameFactory, IStaticDataService staticDataService, IPersistentProgress progress)
        {
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
            _progress = progress;
            

            _progress.MusicSettings.OnMusicSettingsChanged += OnValuesChanged;
        }

        public void PlayMusic(MusicId id) => 
            _gameFactory.Music.Play(_staticDataService.ForMusic(id).Clip);

        public void PlaySound(SoundId id) => 
            _gameFactory.Sounds.Play(_staticDataService.ForSound(id).Clip);

        private void OnValuesChanged()
        {
            MuteMusic(_progress.MusicSettings.MusicMuted && !_progress.MusicSettings.StreamEnabled);
            MuteSounds(_progress.MusicSettings.SoundsMuted);
            SoundsVolume(_progress.MusicSettings.SoundsVolume);
            MusicVolume(_progress.MusicSettings.MusicVolume);
        }

        private void SoundsVolume(float volume) => 
            _staticDataService.AudioMixer.SetFloat(CacheSoundsVolume,MuteDb * (volume-1));

        private void MusicVolume(float volume) => 
            _staticDataService.AudioMixer.SetFloat(CacheMusicVolume,MuteDb * (volume-1));

        public void MuteMusic(bool state)
        {
            if (!state) 
                _gameFactory.Music.Mute();
            else 
                _gameFactory.Music.Unmute();
        }

        private void MuteSounds(bool state)
        {
            if (!state) 
                _gameFactory.Sounds.Mute();
            else 
                _gameFactory.Sounds.Unmute();
        }
    }
}