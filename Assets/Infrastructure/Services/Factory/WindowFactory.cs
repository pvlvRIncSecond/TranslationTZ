using System.Collections.Generic;
using Components;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Progress;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.Windows;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public class WindowFactory : IWindowFactory
    {
        public List<IAudioTrigger> SoundTriggers { get; } = new List<IAudioTrigger>();
        
        private readonly IAssetLoader _assetLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly IAudioService _audioService;
        private readonly IPersistentProgress _progress;
        private Transform _uiRoot;


        public WindowFactory(IAssetLoader assetLoader, IStaticDataService staticDataService, IAudioService audioService, IPersistentProgress progress)
        {
            _assetLoader = assetLoader;
            _staticDataService = staticDataService;
            _audioService = audioService;
            _progress = progress;
        }

        public void CreateUIRoot() =>
            _uiRoot = _assetLoader.Instantiate(AssetPaths.WindowRoot).transform;

        public void CreateSettingsWindow()
        {
            WindowConfig windowConfig = _staticDataService.ForWindow(WindowId.Settings);
            InstantiateRegistered(windowConfig.WindowPrefab,_uiRoot).GetComponentInChildren<SettingsWindow>().Construct(_progress);
        }

        private WindowBase InstantiateRegistered(WindowBase prefab, Transform parent)
        {
            WindowBase windowBase = Object.Instantiate(prefab, parent);
            foreach (IAudioTrigger soundTrigger in windowBase.GetComponentsInChildren<IAudioTrigger>())
            {
                soundTrigger.ConstructAudioTrigger(_audioService);
                SoundTriggers.Add(soundTrigger);
            }

            return windowBase;
        }
    }
}