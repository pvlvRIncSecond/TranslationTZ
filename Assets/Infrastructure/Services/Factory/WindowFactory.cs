using System.Collections.Generic;
using Components;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Audio;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.Windows;
using UnityEngine;

namespace Infrastructure.Services.Factory
{
    public class WindowFactory : IWindowFactory
    {
        public List<ISoundTrigger> SoundTriggers { get; } = new List<ISoundTrigger>();
        
        private readonly IAssetLoader _assetLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly IAudioService _audioService;
        private Transform _uiRoot;


        public WindowFactory(IAssetLoader assetLoader, IStaticDataService staticDataService, IAudioService audioService)
        {
            _assetLoader = assetLoader;
            _staticDataService = staticDataService;
            _audioService = audioService;
        }

        public void CreateUIRoot() =>
            _uiRoot = _assetLoader.Instantiate(AssetPaths.WindowRoot).transform;

        public void CreateSettingsWindow()
        {
            WindowConfig windowConfig = _staticDataService.ForWindow(WindowId.Settings);
            InstantiateRegistered(windowConfig.WindowPrefab,_uiRoot);
        }

        private WindowBase InstantiateRegistered(WindowBase prefab, Transform parent)
        {
            WindowBase windowBase = Object.Instantiate(prefab, parent);
            foreach (ISoundTrigger soundTrigger in windowBase.GetComponentsInChildren<ISoundTrigger>())
            {
                soundTrigger.ConstructAudioTrigger(_audioService);
                SoundTriggers.Add(soundTrigger);
            }

            return windowBase;
        }
    }
}