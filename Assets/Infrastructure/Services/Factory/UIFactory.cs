using Components;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Progress;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public class UIFactory : IuiFactory
    {
        private readonly IAssetLoader _assetLoader;
        private readonly IPersistentProgress _progress;

        private Transform _uiRoot;

        public UIFactory(IAssetLoader assetLoader, IPersistentProgress progress)
        {
            _assetLoader = assetLoader;
            _progress = progress;
        }

        public void CreateUIRoot() =>
            _uiRoot = _assetLoader.Instantiate(AssetPaths.UIRoot).transform;

        public void CreateConnectionIndicator() =>
            _assetLoader.Instantiate(AssetPaths.ConnectionIndicator, _uiRoot).GetComponentInChildren<ServerIndicator>().Construct(_progress);

        public void CreateOdometer() =>
            _assetLoader.Instantiate(AssetPaths.Odometer, _uiRoot).GetComponentInChildren<Odometer>().Construct(_progress);
    }
}