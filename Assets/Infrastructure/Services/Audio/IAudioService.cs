namespace Infrastructure.Services.Audio
{
    public interface IAudioService : IService
    {
        void PlayMusic(MusicId id);
        void PlaySound(SoundId id);
        void MuteMusic(bool state);
    }
}