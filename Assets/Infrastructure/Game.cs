using Infrastructure.StateMachine;

namespace Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine GameStateMachine;

        public Game()
        {
            GameStateMachine = new GameStateMachine();
        }
    }
}