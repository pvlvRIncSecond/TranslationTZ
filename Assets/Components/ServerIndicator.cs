using Infrastructure.Services.Progress;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class ServerIndicator : MonoBehaviour
    {
        [SerializeField] private Image _indicator;
        private IPersistentProgress _progress;

        private void OnDestroy() => 
            _progress.OnConnectedChanged -= SwitchIndicator;

        public void Construct(IPersistentProgress progress)
        {
            _progress = progress;
            _progress.OnConnectedChanged += SwitchIndicator;
        }

        public void SwitchIndicator()
        {
            _indicator.color = _progress.ConnectedToServer ? new Color(0.27f, 0.75f, 0.17f) : new Color(0.67f, 0.17f, 0.15f);
            _indicator.GraphicUpdateComplete();
        }
    }
}
