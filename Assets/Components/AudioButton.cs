using Infrastructure.Services.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class AudioButton : MonoBehaviour, IAudioTrigger
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private SoundId _soundId;

        private IAudioService _audioService;

        private void OnDestroy() => 
            _closeButton.onClick.RemoveListener(PlayAudio);

        public void ConstructAudioTrigger(IAudioService audioService)
        {
            _audioService = audioService;
            _closeButton.onClick.AddListener(PlayAudio);
        }

        private void PlayAudio() => 
            _audioService.PlaySound(_soundId);
    }
}
