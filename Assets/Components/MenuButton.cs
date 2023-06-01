using Infrastructure.Services.Audio;
using Infrastructure.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class MenuButton : MonoBehaviour, ISoundTrigger
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private SoundId _soundId;

        private IWindowService _windowService;
        private IAudioService _audioService;

        public void OnDestroy()
        {
            _menuButton.onClick.RemoveListener(OpenSettingsWindow);
            _menuButton.onClick.RemoveListener(PlayAudio);
        }

        public void Construct(IWindowService windowService)
        {
            _windowService = windowService;
            _menuButton.onClick.AddListener(OpenSettingsWindow);
        }

        public void ConstructAudioTrigger(IAudioService audioService)
        {
            _audioService = audioService;
            _menuButton.onClick.AddListener(PlayAudio);
        }

        public void PlayAudio() => 
            _audioService.PlaySound(_soundId);

        private void OpenSettingsWindow() => 
            _windowService.Open(WindowId.Settings);
    }
}
