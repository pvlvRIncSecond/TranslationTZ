using Infrastructure.Services.Audio;
using Infrastructure.Services.Config;
using Infrastructure.Services.Sockets;

namespace Infrastructure.StateMachine
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IEndpoint _endpoint;
        private readonly IAudioService _audioService;
        private readonly IConfigReader _configReader;

        public GameLoopState(GameStateMachine gameStateMachine, IEndpoint endpoint, IAudioService audioService, IConfigReader configReader)
        {
            _gameStateMachine = gameStateMachine;
            _endpoint = endpoint;
            _audioService = audioService;
            _configReader = configReader;
        }

        public void Enter()
        {
            _configReader.ReadConfig();
            _audioService.PlayMusic(MusicId.Main);
            _endpoint.Connect();
        }

        public void Exit() => 
            _endpoint.Disconnect();
    }
}