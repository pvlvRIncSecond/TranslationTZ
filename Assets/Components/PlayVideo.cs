using System;
using Infrastructure.Services.Progress;
using LibVLCSharp;
using UnityEngine;

namespace Components
{

    public class PlayVideo : MonoBehaviour
    {
        private LibVLC _libVlc;
        private MediaPlayer _mediaPlayer;
        private Texture2D _tex = null;
        private bool _playing;

        private IPersistentProgress _progress;
        private bool _wasMuted;


        public void Construct(IPersistentProgress progress)
        {
            _progress = progress;
            
            _mediaPlayer = new MediaPlayer(_libVlc);
            _mediaPlayer.Media = new Media(new Uri(_progress.StreamAddress));
            
            UpdateAudio();

            _progress.OnStreamChanged += ChangeMedia;
            _progress.MusicSettings.OnMusicSettingsChanged += HandleAudio;
        }

        void Awake()
        {
            Core.Initialize(Application.dataPath);

            _libVlc = new LibVLC(enableDebugLogs: true);

            Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
        }

        void OnDisable()
        {
            _mediaPlayer?.Stop();
            _mediaPlayer?.Dispose();
            _mediaPlayer = null;

            _libVlc?.Dispose();
            _libVlc = null;
        }

        public void PlayPause()
        {
            
            Debug.Log("[VLC] Toggling Play Pause !");
            if (_mediaPlayer == null)
            {
                _mediaPlayer = new MediaPlayer(_libVlc);
            }

            if (_mediaPlayer.IsPlaying)
            {
                _progress.MusicSettings.StreamEnabled = false;
                _mediaPlayer.Pause();
            }
            else
            {
                _playing = true;
                _progress.MusicSettings.StreamEnabled = true;
                
                if (_mediaPlayer.Media == null)
                {
                    _mediaPlayer.Media = new Media(new Uri(_progress.StreamAddress));
                }

                _mediaPlayer.Play();
            }
        }

        void Update()
        {
            if (!_playing) return;

            if (_tex == null)
            {
                // If received size is not null, it and scale the texture
                uint i_videoHeight = 0;
                uint i_videoWidth = 0;

                _mediaPlayer.Size(0, ref i_videoWidth, ref i_videoHeight);
                var texptr = _mediaPlayer.GetTexture(i_videoWidth, i_videoHeight, out bool updated);
                if (i_videoWidth != 0 && i_videoHeight != 0 && updated && texptr != IntPtr.Zero)
                {
                    Debug.Log("Creating texture with height " + i_videoHeight + " and width " + i_videoWidth);
                    _tex = Texture2D.CreateExternalTexture((int)i_videoWidth,
                        (int)i_videoHeight,
                        TextureFormat.RGBA32,
                        false,
                        true,
                        texptr);
                    GetComponent<Renderer>().material.mainTexture = _tex;
                }
            }
            else if (_tex != null)
            {
                var texptr = _mediaPlayer.GetTexture((uint)_tex.width, (uint)_tex.height, out bool updated);
                if (updated)
                {
                    _tex.UpdateExternalTexture(texptr);
                }
            }
        }

        private void HandleAudio() => 
            UpdateAudio();

        private void UpdateAudio()
        {
            _mediaPlayer.Mute = !_progress.MusicSettings.MusicMuted;
            _mediaPlayer.SetVolume((int)(_progress.MusicSettings.MusicVolume  * 100));
        }

        private void ChangeMedia()
        {
            _mediaPlayer.Pause();
            _mediaPlayer.Media = new Media(new Uri(_progress.StreamAddress));
        }
    }
}