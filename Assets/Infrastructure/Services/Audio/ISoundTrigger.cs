namespace Infrastructure.Services.Audio
{
    public interface ISoundTrigger
    {
        public void ConstructAudioTrigger(IAudioService audioService);
    }
}