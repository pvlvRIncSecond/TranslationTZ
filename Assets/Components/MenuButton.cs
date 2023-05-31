using Infrastructure.Services.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private Button _menuButton;
        private IWindowService _windowService;

        public void OnDestroy() => 
            _menuButton.onClick.RemoveListener(OpenSettingsWindow);

        public void Construct(IWindowService windowService)
        {
            _windowService = windowService;
            _menuButton.onClick.AddListener(OpenSettingsWindow);
        }

        private void OpenSettingsWindow() => 
            _windowService.Open(WindowId.Settings);
    }
}
