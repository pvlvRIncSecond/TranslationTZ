using System.Collections;
using Infrastructure.Services.Progress;
using UnityEngine;

namespace Components
{
    public class Odometer : MonoBehaviour
    {
        private const float LerpRatio = .1f;
        [SerializeField] private TMPro.TextMeshProUGUI _odometer;

        private float _currentValue;
        private IPersistentProgress _persistentProgress;

        public void Awake() =>
            _currentValue = 0;

        public void Start() =>
            StartCoroutine(UpdateOdometer());

        public void OnDestroy() =>
            StopAllCoroutines();

        public void Construct(IPersistentProgress persistentProgress) =>
            _persistentProgress = persistentProgress;

        private IEnumerator UpdateOdometer()
        {
            while (true)
            {
                _currentValue = _persistentProgress.Odometer != 0
                    ? Mathf.Lerp(_currentValue, _persistentProgress.Odometer, LerpRatio)
                    : 0;
                _odometer.text = _currentValue.ToString();
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}