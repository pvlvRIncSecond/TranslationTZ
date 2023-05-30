using Infrastructure.Services.Sockets;

namespace Infrastructure.StateMachine
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IEndpoint _endpoint;

        public GameLoopState(GameStateMachine gameStateMachine, IEndpoint endpoint)
        {
            _gameStateMachine = gameStateMachine;
            _endpoint = endpoint;
        }

        public void Enter() => 
            _endpoint.Try();

        public void Exit()
        {
        }
    }
}