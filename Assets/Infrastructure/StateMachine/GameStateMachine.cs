using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IState> _states;
        private IState _currentState;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(),
            };
        }

        public void Enter<TState>() where TState : IState
        {
            LogWhatState<TState>();

            _currentState?.Exit();
            IState state = _states[typeof(TState)];
            state.Enter();
        }

        private void LogWhatState<TState>() where TState : IState => 
            Debug.Log($"Entered {typeof(TState)} state.");
    }
}