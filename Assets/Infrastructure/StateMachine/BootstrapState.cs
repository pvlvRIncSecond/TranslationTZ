using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";

        public void Enter()
        {
            SceneManager.LoadScene(Initial);
            RegisterServices();
        }

        public void Exit()
        {
        }

        private void RegisterServices() => 
            Debug.Log("Services registered");
    }
}