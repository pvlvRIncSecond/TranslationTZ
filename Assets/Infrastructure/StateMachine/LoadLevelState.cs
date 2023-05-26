using Infrastructure.Scenes;

namespace Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneName) => 
            _sceneLoader.Load(sceneName, OnLoad);

        public void Exit()
        {
        }

        private void OnLoad()
        {
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}