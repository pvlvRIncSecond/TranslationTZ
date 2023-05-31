using System;
using Components;

namespace Infrastructure.Services.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase WindowPrefab;
    }
}