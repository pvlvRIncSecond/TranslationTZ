using Components;
using Components.Audio;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Progress;

namespace Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        public MusicSource Music { get; private set; }
        public SoundsSource Sounds { get; private set; }
        public PlayVideo VideoPanel { get; private set; }

        private readonly IAssetLoader _assetLoader;
        private readonly IPersistentProgress _progress;


        public GameFactory(IAssetLoader assetLoader, IPersistentProgress progress)
        {
            _assetLoader = assetLoader;
            _progress = progress;
        }

        public void CreateMusicSource() =>
            Music = _assetLoader.Instantiate(AssetPaths.MusicSource).GetComponentInChildren<MusicSource>();

        public void CreateSoundsSource() =>
            Sounds = _assetLoader.Instantiate(AssetPaths.SoundsSource).GetComponentInChildren<SoundsSource>();

        public void CreateVideoPanel()
        {
            VideoPanel = _assetLoader.Instantiate(AssetPaths.VideoPanel).GetComponentInChildren<PlayVideo>();
            VideoPanel.Construct(_progress);
        }
    }
}