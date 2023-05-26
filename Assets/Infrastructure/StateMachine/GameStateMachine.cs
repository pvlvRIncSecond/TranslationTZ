using System;
using System.Collections.Generic;
using Infrastructure.Scenes;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IState _currentState;

        public GameStateMachine(SceneLoader sceneLoader)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
                [typeof(GameLoopState)] = new GameLoopState(this),
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