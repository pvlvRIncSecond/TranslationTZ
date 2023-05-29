using Infrastructure.Services.Assets;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public class UIFactory : IuiFactory
    {
        private readonly IAssetLoader _assetLoader;

        private Transform _uiRoot;

        public UIFactory(IAssetLoader assetLoader) =>
            _assetLoader = assetLoader;

        public void CreateUIRoot() =>
            _uiRoot = _assetLoader.Instantiate(AssetPaths.UIRoot).transform;

        public void CreateConnectionIndicator() =>
            _assetLoader.Instantiate(AssetPaths.ConnectionIndicator, _uiRoot);
    }
}