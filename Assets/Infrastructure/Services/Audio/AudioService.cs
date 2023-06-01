using Infrastructure.Services.Factory;
using Infrastructure.Services.StaticData;

namespace Infrastructure.Services.Audio
{
    public class AudioService : IAudioService
    {
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticDataService;

        public AudioService(IGameFactory gameFactory, IStaticDataService staticDataService)
        {
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
        }

        public void PlayMusic(MusicId id) => 
            _gameFactory.Music.Play(_staticDataService.ForMusic(id).Clip);

        public void PlaySound(SoundId id) => 
            _gameFactory.Sounds.Play(_staticDataService.ForSound(id).Clip);
    }
}