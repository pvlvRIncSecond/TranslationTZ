using Components.Audio;
using Infrastructure.Services.Assets;

namespace Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        public MusicSource Music { get; private set; }
        public SoundsSource Sounds { get; private set; }

        private readonly IAssetLoader _assetLoader;

        public GameFactory(IAssetLoader assetLoader) =>
            _assetLoader = assetLoader;

        public void CreateMusicSource() =>
            Music = _assetLoader.Instantiate(AssetPaths.MusicSource).GetComponentInChildren<MusicSource>();

        public void CreateSoundsSource() =>
            Sounds = _assetLoader.Instantiate(AssetPaths.SoundsSource).GetComponentInChildren<SoundsSource>();
    }
}