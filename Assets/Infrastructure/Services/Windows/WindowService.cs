using Infrastructure.Services.Factory;

namespace Infrastructure.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IWindowFactory _windowFactory;

        public WindowService(IWindowFactory windowFactory) => 
            _windowFactory = windowFactory;

        public void Open(WindowId id)
        {
            switch (id)
            {
                case WindowId.Unknown:
                    break;
                case WindowId.Settings:
                    _windowFactory.CreateSettingsWindow();
                    break;
            }
        }
    }
}