namespace Infrastructure.Services.Sockets
{
    public interface IEndpoint : IService
    {
        void Connect();
        void Disconnect();
        void Reconnect();
    }
}