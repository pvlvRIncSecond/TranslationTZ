namespace Infrastructure.Services.Factory
{
    public interface IWindowFactory : IFactory
    {
        void CreateSettingsWindow();
        void CreateUIRoot();
    }
}