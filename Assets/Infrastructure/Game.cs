using Infrastructure.Scenes;
using Infrastructure.Services;
using Infrastructure.StateMachine;

namespace Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine GameStateMachine;

        public Game(ICoroutineRunner coroutineRunner, ServiceLocator serviceLocator) => 
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), serviceLocator);
    }
}