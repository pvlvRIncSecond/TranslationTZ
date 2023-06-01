using System;
using System.Collections.Generic;
using Infrastructure.Scenes;
using Infrastructure.Services;
using Infrastructure.Services.Audio;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Sockets;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private readonly ServiceLocator _serviceLocator;
        
        private IState _currentState;

        public GameStateMachine(SceneLoader sceneLoader, ServiceLocator serviceLocator, ICoroutineRunner coroutineRunner)
        {
            _serviceLocator = serviceLocator;
            
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, _serviceLocator, coroutineRunner),
                [typeof(LoadLevelState)] = new LoadLevelState(
                    this, 
                    sceneLoader, 
                    _serviceLocator.Single<IuiFactory>(), 
                    _serviceLocator.Single<IWindowFactory>(),
                    _serviceLocator.Single<IGameFactory>()
                    ),
                [typeof(GameLoopState)] = new GameLoopState(this, _serviceLocator.Single<IEndpoint>(), _serviceLocator.Single<IAudioService>()),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState,TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            IPayloadedState<TPayload> state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            LogWhatState<TState>();
            _currentState?.Exit();
            
            return GetState<TState>();;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;

        private void LogWhatState<TState>() where TState : IExitableState => 
            Debug.Log($"Entered {typeof(TState)} state.");
    }
}