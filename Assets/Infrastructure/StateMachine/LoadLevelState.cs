using Infrastructure.Scenes;
using Infrastructure.Services.Factory;

namespace Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IuiFactory _uiFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IuiFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName) => 
            _sceneLoader.Load(sceneName, OnLoad);

        public void Exit()
        {
        }

        private void OnLoad()
        {
            _uiFactory.CreateUIRoot();
            _uiFactory.CreateConnectionIndicator();
            
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}