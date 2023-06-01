using System.Collections.Generic;
using Components;
using Components.Audio;
using Infrastructure.Services.Audio;

namespace Infrastructure.Services.Factory
{
    public interface IFactory : IService
    {
    }

    public interface IGameFactory : IFactory
    {
        void CreateMusicSource();
        void CreateSoundsSource();
        MusicSource Music { get; }
        SoundsSource Sounds { get; }
    }

    public interface IuiFactory : IFactory
    {
        List<ISoundTrigger> SoundTriggers { get; }
        void CreateUIRoot();
        void CreateConnectionIndicator();
        void CreateOdometer();
        void CreateMenuButton();
    }
}