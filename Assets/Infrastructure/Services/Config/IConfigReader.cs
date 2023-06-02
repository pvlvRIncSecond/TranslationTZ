namespace Infrastructure.Services.Config
{
    public interface IConfigReader : IService
    {
        void ReadConfig();
    }
}