using System;

namespace Infrastructure.Services.Progress
{
    public interface IPersistentProgress : IService
    {
        float Odometer { get; set; }
        bool ConnectedToServer { get; set; }
        Action OnConnectedChanged { get; set; }
        MusicSettings MusicSettings { get; set; }
        string ServerAddress { get; set; }
        string ServerPort { get; set; }
        string StreamAddress { get; set; }
        string Endpoint();
    }
}