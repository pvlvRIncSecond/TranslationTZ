using Infrastructure.Scenes;
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
        }

        public void Enter() =>
            _sceneLoader.Load(Initial, OnLoad);

        public void Exit()
        {
        }

        private void OnLoad()
        {
            RegisterServices();
            _gameStateMachine.Enter<LoadLevelState,string>(Main);
        }


        private void RegisterServices() =>
            Debug.Log("Services registered");
    }
}