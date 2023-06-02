using System.Collections;
using System.Timers;
using Infrastructure.Services.Progress;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class ServerIndicator : MonoBehaviour
    {
        [SerializeField] private Image _indicator;
        [SerializeField] private TMPro.TextMeshProUGUI _reconnectText;
        private IPersistentProgress _progress;

        private void OnDestroy() =>
            _progress.OnConnectedChanged -= SwitchIndicator;

        public void Construct(IPersistentProgress progress)
        {
            _progress = progress;
            _progress.OnConnectedChanged += SwitchIndicator;
            _progress.OnConnectedChanged += AnimateText;

            _reconnectText.alpha = 0;
        }

        private void AnimateText()
        {
            if (_progress.ConnectedToServer)
            {
                StopAllCoroutines();
                _reconnectText.alpha = 0;
            }
            else
            {
                StartCoroutine(AnimateAlpha());
            }
        }

        private IEnumerator AnimateAlpha()
        {
            while (true)
            {
                _reconnectText.alpha = (Mathf.Sin(Time.time) + 1) / 2;
                yield return null;
            }
        }

        private void SwitchIndicator()
        {
            _indicator.color = _progress.ConnectedToServer
                ? new Color(0.27f, 0.75f, 0.17f)
                : new Color(0.67f, 0.17f, 0.15f);
            _indicator.GraphicUpdateComplete();
        }
    }
}