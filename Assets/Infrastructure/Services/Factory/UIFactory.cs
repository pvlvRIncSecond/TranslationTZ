using System.Collections.Generic;
using Components;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Progress;
using Infrastructure.Services.Windows;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public class UIFactory : IuiFactory
    {
        public List<IAudioTrigger> SoundTriggers { get; } = new List<IAudioTrigger>();

        private readonly IAssetLoader _assetLoader;
        private readonly IPersistentProgress _progress;
        private readonly IWindowService _windowService;
        private readonly IAudioService _audioService;

        private Transform _uiRoot;

        public UIFactory(IAssetLoader assetLoader, IPersistentProgress progress, IWindowService windowService, IAudioService audioService)
        {
            _assetLoader = assetLoader;
            _progress = progress;
            _windowService = windowService;
            _audioService = audioService;
        }

        public void CreateUIRoot() =>
            _uiRoot = _assetLoader.Instantiate(AssetPaths.UIRoot).transform;

        public void CreateConnectionIndicator() =>
            _assetLoader.Instantiate(AssetPaths.ConnectionIndicator, _uiRoot).GetComponentInChildren<ServerIndicator>().Construct(_progress);

        public void CreateOdometer() =>
            _assetLoader.Instantiate(AssetPaths.Odometer, _uiRoot).GetComponentInChildren<Odometer>().Construct(_progress);

        public void CreateMenuButton() =>
            InstantiateRegistered(AssetPaths.MenuButton, _uiRoot).GetComponentInChildren<MenuButton>().Construct(_windowService);

        public void CreateStreamButton(PlayVideo videoPanel) => 
            _assetLoader.Instantiate(AssetPaths.StreamButton, _uiRoot).GetComponentInChildren<StreamButton>().Construct(videoPanel);

        private GameObject InstantiateRegistered(string path, Transform parent)
        {
            GameObject gameObject = _assetLoader.Instantiate(path, parent);
            foreach (IAudioTrigger soundTrigger in gameObject.GetComponentsInChildren<IAudioTrigger>())
            {
                soundTrigger.ConstructAudioTrigger(_audioService);
                SoundTriggers.Add(soundTrigger);
            }

            return gameObject;
        }
    }
}