using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Windows;
using UnityEngine;
using UnityEngine.Audio;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        private Dictionary<MusicId, MusicConfig> _musicConfigs;
        private Dictionary<SoundId, SoundConfig> _soundsConfigs;
        private AudioMixer _master;

        private const string StaticDataWindowsPath = "StaticData/Windows/WindowData";
        private const string StaticDataMusicPath = "StaticData/Audio/MusicData";
        private const string StaticDataSoundsPath = "StaticData/Audio/SoundData";
        private const string AudioMixerPath = "Audio/Master";

        public void LoadStaticData()
        {
            _windowConfigs = Resources
                .Load<WindowStaticData>(StaticDataWindowsPath)
                .WindowConfigs
                .ToDictionary(x => x.WindowId, x => x);

            _musicConfigs = Resources
                .Load<MusicStaticData>(StaticDataMusicPath)
                .MusicConfigs
                .ToDictionary(x => x.MusicId, x => x);

            _soundsConfigs = Resources
                .Load<SoundsStaticData>(StaticDataSoundsPath)
                .SoundConfigs
                .ToDictionary(x => x.SoundId, x => x);

            _master = Resources.Load<AudioMixer>(AudioMixerPath);
        }

        public WindowConfig ForWindow(WindowId id) =>
            _windowConfigs.TryGetValue(id, out WindowConfig windowConfig)
                ? windowConfig
                : null;

        public MusicConfig ForMusic(MusicId id) =>
            _musicConfigs.TryGetValue(id, out MusicConfig windowConfig)
                ? windowConfig
                : null;

        public SoundConfig ForSound(SoundId id) =>
            _soundsConfigs.TryGetValue(id, out SoundConfig windowConfig)
                ? windowConfig
                : null;

        public AudioMixer AudioMixer => _master;
    }
}