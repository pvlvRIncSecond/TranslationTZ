using Infrastructure.Services.Audio;
using Infrastructure.Services.Sockets;

namespace Infrastructure.StateMachine
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IEndpoint _endpoint;
        private readonly IAudioService _audioService;

        public GameLoopState(GameStateMachine gameStateMachine, IEndpoint endpoint, IAudioService audioService)
        {
            _gameStateMachine = gameStateMachine;
            _endpoint = endpoint;
            _audioService = audioService;
        }

        public void Enter()
        {
            _audioService.PlayMusic(MusicId.Main);
            _endpoint.Connect();
        }

        public void Exit() => 
            _endpoint.Disconnect();
    }
}