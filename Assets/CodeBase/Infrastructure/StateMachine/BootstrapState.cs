using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services;
using CodeBase.Services.Ad;
using CodeBase.Services.Factory;
using CodeBase.Services.Input;
using CodeBase.Services.Progress;
using CodeBase.Services.SaveLoad;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.Infrastructure.StateMachine
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly ServiceLocator _serviceLocator;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, ServiceLocator serviceLocator)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _serviceLocator = serviceLocator;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticDataService();
            RegisterAdService();
            
            _serviceLocator.RegisterSingleInstance<IGameStateMachine>(_stateMachine);
            _serviceLocator.RegisterSingleInstance<IInputService>(GetInputService());
            _serviceLocator.RegisterSingleInstance<IAssetProvider>(new AssetProvider());
            _serviceLocator.RegisterSingleInstance<IStorageProgressService>(new StorageProgressService());
            
            _serviceLocator.RegisterSingleInstance<IUIFactory>(new UIFactory(
                _serviceLocator.GetSingleInstance<IAssetProvider>(),
                _serviceLocator.GetSingleInstance<IStaticDataService>(),
                _serviceLocator.GetSingleInstance<IStorageProgressService>(), _serviceLocator.GetSingleInstance<IAdService>()));
            _serviceLocator.RegisterSingleInstance<IWindowService>(new WindowService(_serviceLocator.GetSingleInstance<IUIFactory>()));
            
            _serviceLocator.RegisterSingleInstance<IGameFactory>(new GameFactory(
                _serviceLocator.GetSingleInstance<IAssetProvider>(), 
                _serviceLocator.GetSingleInstance<IStaticDataService>(), 
                _serviceLocator.GetSingleInstance<IStorageProgressService>(), 
                _serviceLocator.GetSingleInstance<IWindowService>()
            ));
            
            _serviceLocator.RegisterSingleInstance<ISaveLoadService>(new SaveLoadService(
                _serviceLocator.GetSingleInstance<IStorageProgressService>(), 
                _serviceLocator.GetSingleInstance<IGameFactory>()
            ));
        }

        private void RegisterAdService()
        {
            var adService = new AdService();
            adService.Init();
            _serviceLocator.RegisterSingleInstance<IAdService>(adService);
        }

        private void RegisterStaticDataService()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadResources();
            _serviceLocator.RegisterSingleInstance<IStaticDataService>(staticData);
        }

        public void Exit()
        {
            
        }
        
        private static IInputService GetInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }
    }
}