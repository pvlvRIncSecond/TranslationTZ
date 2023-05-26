using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class ServerIndicator : MonoBehaviour
    {
        [SerializeField] private Image _indicator;

        public void SwitchIndicator(bool to) => 
            _indicator.color = to ? new Color(74, 183, 53) : new Color(170, 36, 0);
    }
}
