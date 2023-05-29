using Infrastructure.Scenes;
using Infrastructure.Services;
using Infrastructure.Services.Factory;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private const string Main = "Main";
        private const string Initial = "Initial";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly ServiceLocator _serviceLocator;
        private SceneLoader _sceneLoader;


        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ServiceLocator serviceLocator)
        {
            _gameStateMachine = gameStateMachine;
            _serviceLocator = serviceLocator;
            _sceneLoader = sceneLoader;

            RegisterServices();
        }

        public void Enter() =>
            _sceneLoader.Load(Initial, OnLoad);

        public void Exit()
        {
        }

        private void OnLoad() => 
            _gameStateMachine.Enter<LoadLevelState,string>(Main);


        private void RegisterServices()
        {
            _serviceLocator.RegisterSingle<IuiFactory>( new UIFactory());
            
            Debug.Log("Services registered");
        }
    }
}