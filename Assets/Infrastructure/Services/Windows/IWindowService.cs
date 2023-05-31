namespace Infrastructure.Services.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowId id);
    }
}