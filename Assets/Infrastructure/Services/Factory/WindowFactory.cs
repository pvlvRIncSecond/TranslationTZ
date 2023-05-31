using Infrastructure.Services.Assets;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.Windows;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public class WindowFactory : IWindowFactory
    {
        private readonly IAssetLoader _assetLoader;
        private readonly IStaticDataService _staticDataService;
        private Transform _uiRoot;

        public WindowFactory(IAssetLoader assetLoader, IStaticDataService staticDataService)
        {
            _assetLoader = assetLoader;
            _staticDataService = staticDataService;
        }

        public void CreateUIRoot() =>
            _uiRoot = _assetLoader.Instantiate(AssetPaths.WindowRoot).transform;
        
        public void CreateSettingsWindow()
        {
            WindowConfig windowConfig = _staticDataService.ForWindow(WindowId.Settings);
            Object.Instantiate(windowConfig.WindowPrefab,_uiRoot);
        }
    }
}