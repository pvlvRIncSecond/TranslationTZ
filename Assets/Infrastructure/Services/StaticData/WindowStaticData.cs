using System.Collections.Generic;
using Infrastructure.Services.Windows;
using UnityEngine;

namespace Infrastructure.Services.StaticData
{
    [CreateAssetMenu(menuName = "Static Data/Window Static Data", fileName = "WindowData")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> WindowConfigs;
    }
}