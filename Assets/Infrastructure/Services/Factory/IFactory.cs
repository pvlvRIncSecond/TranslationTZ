namespace Infrastructure.Services.Factory
{
    public interface IFactory : IService
    {
    }

    public interface IGameFactory : IFactory
    {
    }

    public interface IuiFactory : IFactory
    {
        void CreateUIRoot();
        void CreateConnectionIndicator();
        void CreateOdometer();
        void CreateMenuButton();
    }
}