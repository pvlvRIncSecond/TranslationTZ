using Infrastructure.Scenes;
using Infrastructure.Services;
using Infrastructure.Services.Sockets;
using Infrastructure.StateMachine;
using UnityEngine;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game(this, ServiceLocator.Container);
            _game.GameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
        
        private void OnApplicationQuit() => 
            ServiceLocator.Container.Single<IEndpoint>().Disconnect();
    }
}