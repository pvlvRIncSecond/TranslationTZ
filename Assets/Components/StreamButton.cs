using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class StreamButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private PlayVideo _videoPanel;
        private Renderer _rendererPanel;

        public void Construct(PlayVideo videoPanel)
        {
            _videoPanel = videoPanel;
            _rendererPanel = _videoPanel.GetComponent<Renderer>();
            _button.onClick.AddListener(Play);
        }

        private void Play()
        {
            _rendererPanel.enabled = true;
            _videoPanel.PlayPause();
        }
    }
}