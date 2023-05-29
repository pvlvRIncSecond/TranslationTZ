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
        private SceneLoader _sceneLoader;


        public BootstrapState(GameStateMachine gameStateMachine ,SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
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
            ServiceLocator.Container.RegisterSingle<IuiFactory>( new UIFactory());
            
            Debug.Log("Services registered");
        }
    }
}