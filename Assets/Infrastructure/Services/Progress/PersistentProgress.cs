using System;

namespace Infrastructure.Services.Progress
{
    public class PersistentProgress : IPersistentProgress
    {
        public Action OnConnectedChanged { get; set; }
        public Action OnServerChanged { get; set; }
        public Action OnStreamChanged { get; set; }

        public MusicSettings MusicSettings { get; set; }
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

        public string ServerAddress {
            get => _serverAddress;
            set
            {
                Odometer = 0;
                _serverAddress = value;
                OnServerChanged?.Invoke();
            }
        }
        public string ServerPort {
            get => _serverPort;
            set
            {
                Odometer = 0;
                _serverPort = value;
                OnServerChanged?.Invoke();
            }
        }

        public string StreamAddress {
            get => _streamAddress;
            set
            {
                _streamAddress = value;
                OnStreamChanged?.Invoke();
            }
        }

        private string _serverAddress;
        private string _serverPort;
        private string _streamAddress;
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