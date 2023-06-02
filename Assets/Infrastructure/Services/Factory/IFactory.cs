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
        PlayVideo VideoPanel { get; }
        void CreateVideoPanel();
    }

    public interface IuiFactory : IFactory
    {
        List<IAudioTrigger> SoundTriggers { get; }
        void CreateUIRoot();
        void CreateConnectionIndicator();
        void CreateOdometer();
        void CreateMenuButton();
        void CreateStreamButton(PlayVideo videoPanel);
    }
}