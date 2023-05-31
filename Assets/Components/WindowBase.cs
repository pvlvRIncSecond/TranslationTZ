using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class WindowBase : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void Awake() => 
            OnAwake();

        protected virtual void OnAwake() => 
            _closeButton.onClick.AddListener(()=>Destroy(gameObject));
    }
}