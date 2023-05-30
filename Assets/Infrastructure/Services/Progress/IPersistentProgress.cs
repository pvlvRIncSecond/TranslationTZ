using System;

namespace Infrastructure.Services.Progress
{
    public interface IPersistentProgress : IService
    {
        float Odometer { get; set; }
        bool ConnectedToServer { get; set; }
        Action OnConnectedChanged { get; set; }
    }
}