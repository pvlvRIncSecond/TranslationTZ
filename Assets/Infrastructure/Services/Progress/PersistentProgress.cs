using System;

namespace Infrastructure.Services.Progress
{
    public class PersistentProgress : IPersistentProgress
    {
        public Action OnConnectedChanged { get; set; }
        
        public float Odometer { get; set; }
        public bool ConnectedToServer
        {
            get => _connectedToServer;
            set
            {
                _connectedToServer = value;
                OnConnectedChanged?.Invoke();
            }
        }

        public MusicSettings MusicSettings
        {
            get => _musicSettings;
            set
            {
                _musicSettings = value;
            }
        }

        private bool _connectedToServer;

        private MusicSettings _musicSettings;
        
        public PersistentProgress()
        {
            Odometer = 0;
            _musicSettings = new MusicSettings();
        }
    }
}