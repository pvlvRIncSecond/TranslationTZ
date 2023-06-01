using Infrastructure.Scenes;
using Infrastructure.Services;
using Infrastructure.Services.Assets;
using Infrastructure.Services.Audio;
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
        private readonly ServiceLocator _services;
        private readonly ICoroutineRunner _coroutineRunner;
        private SceneLoader _sceneLoader;


        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ServiceLocator services,
            ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _services = services;
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
            _services.RegisterSingle<IStaticDataService>(new StaticDataService());
            _services.Single<IStaticDataService>().LoadStaticData();

            _services.RegisterSingle<IAssetLoader>(new AssetLoader());
            _services.RegisterSingle<IPersistentProgress>(new PersistentProgress());

            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetLoader>()));
            _services.RegisterSingle<IAudioService>(new AudioService(_services.Single<IGameFactory>(),
                _services.Single<IStaticDataService>(), _services.Single<IPersistentProgress>()));

            _services.RegisterSingle<IEndpoint>(new Endpoint(
                _coroutineRunner,
                _services.Single<IPersistentProgress>()
            ));

            _services.RegisterSingle<IWindowFactory>(new WindowFactory(
                _services.Single<IAssetLoader>(),
                _services.Single<IStaticDataService>(),
                _services.Single<IAudioService>(),
                _services.Single<IPersistentProgress>()
            ));

            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IWindowFactory>()));

            _services.RegisterSingle<IuiFactory>(new UIFactory(
                _services.Single<IAssetLoader>(),
                _services.Single<IPersistentProgress>(),
                _services.Single<IWindowService>(),
                _services.Single<IAudioService>()
            ));

            Debug.Log("Services registered");
        }
    }
}