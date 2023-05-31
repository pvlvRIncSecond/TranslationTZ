using Components;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Progress;
using Infrastructure.Services.Windows;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public class UIFactory : IuiFactory
    {
        private readonly IAssetLoader _assetLoader;
        private readonly IPersistentProgress _progress;
        private readonly IWindowService _windowService;

        private Transform _uiRoot;

        public UIFactory(IAssetLoader assetLoader, IPersistentProgress progress, IWindowService windowService)
        {
            _assetLoader = assetLoader;
            _progress = progress;
            _windowService = windowService;
        }

        public void CreateUIRoot() =>
            _uiRoot = _assetLoader.Instantiate(AssetPaths.UIRoot).transform;

        public void CreateConnectionIndicator() =>
            _assetLoader.Instantiate(AssetPaths.ConnectionIndicator, _uiRoot).GetComponentInChildren<ServerIndicator>().Construct(_progress);

        public void CreateOdometer() =>
            _assetLoader.Instantiate(AssetPaths.Odometer, _uiRoot).GetComponentInChildren<Odometer>().Construct(_progress);

        public void CreateMenuButton() =>
            _assetLoader.Instantiate(AssetPaths.MenuButton, _uiRoot).GetComponentInChildren<MenuButton>().Construct(_windowService);

        public void CreateSettingsWindow() => 
            _assetLoader.Instantiate(AssetPaths.Settings, _uiRoot).GetComponentInChildren<SettingsWindow>().Construct();
    }
}