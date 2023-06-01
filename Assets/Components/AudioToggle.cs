using Infrastructure.Services.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class AudioToggle : MonoBehaviour, IAudioTrigger
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private SoundId _soundId;
    
        private IAudioService _audioService;

        private void OnDestroy() => 
            _toggle.onValueChanged.RemoveListener(PlayAudio);

        public void ConstructAudioTrigger(IAudioService audioService)
        {
            _audioService = audioService;
            _toggle.onValueChanged.AddListener(PlayAudio);
        }

        private void PlayAudio(bool state) => 
            _audioService.PlaySound(_soundId);

    }
}
