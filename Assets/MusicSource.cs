using UnityEngine;

public class MusicSource : MonoBehaviour
{
    [SerializeField] private AudioSource audio;

    public void Mute() => 
        audio.Stop();

    public void Unmute() => 
        audio.Play();
}
