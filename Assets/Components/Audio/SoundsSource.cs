using UnityEngine;

namespace Components.Audio
{
    public class SoundsSource : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void Play(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        public void Mute() =>
            _audioSource.mute = true;

        public void Unmute() =>
            _audioSource.mute = false;
    }
}
