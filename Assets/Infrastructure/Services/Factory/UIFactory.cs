using UnityEngine;

namespace Infrastructure.Services.Factory
{
    internal class UIFactory : IuiFactory
    {
        private const string UIRoot = "UIRoot";
        private const string ConnectionIndicator = "ConnectionIndicator";

        private Transform _uiRoot;
        
        public void CreateUIRoot() => 
            _uiRoot =Object.Instantiate(Resources.Load<GameObject>(UIRoot)).transform;

        public void CreateConnectionIndicator() => 
            Object.Instantiate(Resources.Load<GameObject>(ConnectionIndicator),_uiRoot);
    }
}