using Infrastructure.Services.Audio;
using Infrastructure.Services.Windows;
using UnityEngine.Audio;

namespace Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadStaticData();
        WindowConfig ForWindow(WindowId id);
        MusicConfig ForMusic(MusicId id);
        SoundConfig ForSound(SoundId id);
        AudioMixer AudioMixer { get; }
    }
}