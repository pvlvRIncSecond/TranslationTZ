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

        private bool _connectedToServer;

        public PersistentProgress() => 
            Odometer = 0;
    }
}