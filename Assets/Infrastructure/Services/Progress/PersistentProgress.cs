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

        public MusicSettings MusicSettings { get; set; }

        public string ServerAddress { get; set; }
        public string ServerPort { get; set; }

        public string StreamAddress { get; set; }

        private bool _connectedToServer;

        public PersistentProgress()
        {
            Odometer = 0;
            MusicSettings = new MusicSettings();
        }
        
        public string Endpoint() => 
            $"ws://{ServerAddress}:{ServerPort}/ws";
    }
}