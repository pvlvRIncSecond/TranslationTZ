using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.Windows;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WindowId, WindowConfig> _windowConfigs;
        private const string StaticDataWindowsPath = "StaticData/Windows/WindowData";

        public void LoadStaticData()
        {
            _windowConfigs = Resources
                .Load<WindowStaticData>(StaticDataWindowsPath)
                .WindowConfigs
                .ToDictionary(x => x.WindowId, x => x);
        }
        
        public WindowConfig ForWindow(WindowId windowId) =>
            _windowConfigs.TryGetValue(windowId, out WindowConfig windowConfig)
                ? windowConfig
                : null;
    }
}