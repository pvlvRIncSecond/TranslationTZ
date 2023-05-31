using Infrastructure.Scenes;
using Infrastructure.Services.Factory;

namespace Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IuiFactory _uiFactory;
        private readonly IWindowFactory _windowFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IuiFactory uiFactory, IWindowFactory windowFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _windowFactory = windowFactory;
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
            _uiFactory.CreateOdometer();
            _uiFactory.CreateMenuButton();

            _windowFactory.CreateUIRoot();
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}