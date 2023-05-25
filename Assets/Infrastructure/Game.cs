using Infrastructure.Scenes;
using Infrastructure.StateMachine;

namespace Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine GameStateMachine;

        public Game(ICoroutineRunner coroutineRunner) => 
            GameStateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
    }
}