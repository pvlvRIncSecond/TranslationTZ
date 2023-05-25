using Infrastructure.Scenes;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private SceneLoader _sceneLoader;

        public BootstrapState(SceneLoader sceneLoader) => 
            _sceneLoader = sceneLoader;

        private const string Initial = "Initial";

        public void Enter() => 
            _sceneLoader.Load(Initial, OnLoad);

        public void Exit()
        {
        }

        private void OnLoad() =>
            RegisterServices();

        private void RegisterServices() =>
            Debug.Log("Services registered");
    }
}