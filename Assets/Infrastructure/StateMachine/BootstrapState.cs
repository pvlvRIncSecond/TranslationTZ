using Infrastructure.Scenes;
using Infrastructure.Services;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Factory;
using Infrastructure.Services.Progress;
using Infrastructure.Services.Sockets;
using Infrastructure.Services.StaticData;
using Infrastructure.Services.Windows;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private const string Main = "Main";
        private const string Initial = "Initial";

        private readonly GameStateMachine _gameStateMachine;
        private readonly ServiceLocator _serviceLocator;
        private readonly ICoroutineRunner _coroutineRunner;
        private SceneLoader _sceneLoader;


        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ServiceLocator serviceLocator,
            ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _serviceLocator = serviceLocator;
            _coroutineRunner = coroutineRunner;
            _sceneLoader = sceneLoader;

            RegisterServices();
        }

        public void Enter() =>
            _sceneLoader.Load(Initial, OnLoad);

        public void Exit()
        {
        }

        private void OnLoad() =>
            _gameStateMachine.Enter<LoadLevelState, string>(Main);


        private void RegisterServices()
        {
            _serviceLocator.RegisterSingle<IStaticDataService>(new StaticDataService());
            _serviceLocator.Single<IStaticDataService>().LoadStaticData();
                
            _serviceLocator.RegisterSingle<IAssetLoader>(new AssetLoader());
            _serviceLocator.RegisterSingle<IPersistentProgress>(new PersistentProgress());

            _serviceLocator.RegisterSingle<IEndpoint>(new Endpoint(
                _coroutineRunner,
                _serviceLocator.Single<IPersistentProgress>()
            ));

            _serviceLocator.RegisterSingle<IWindowFactory>(new WindowFactory(
                _serviceLocator.Single<IAssetLoader>(),
                _serviceLocator.Single<IStaticDataService>()
            ));

            _serviceLocator.RegisterSingle<IWindowService>(new WindowService(_serviceLocator.Single<IWindowFactory>()));

            _serviceLocator.RegisterSingle<IuiFactory>(new UIFactory(
                _serviceLocator.Single<IAssetLoader>(),
                _serviceLocator.Single<IPersistentProgress>(),
                _serviceLocator.Single<IWindowService>()
            ));

            Debug.Log("Services registered");
        }
    }
}