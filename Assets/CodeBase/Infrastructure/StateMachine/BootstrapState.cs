using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services;
using CodeBase.Services.Ad;
using CodeBase.Services.Factory;
using CodeBase.Services.Input;
using CodeBase.Services.Progress;
using CodeBase.Services.SaveLoad;
using CodeBase.Services.Words;
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
            IStaticDataService staticData = RegisterStaticDataService();
            IAdService adService = RegisterAdService();
            
            _serviceLocator.RegisterSingleInstance<IGameStateMachine>(_stateMachine);
            _serviceLocator.RegisterSingleInstance<IInputService>(GetInputService());
            
            IAssetProvider assetProvider = _serviceLocator.RegisterSingleInstance<IAssetProvider>(new AssetProvider());
            IProgressService progressService = _serviceLocator.RegisterSingleInstance<IProgressService>(new ProgressService());
            
            IWordService wordService = RegisterWordService();
                
            IUIFactory uiFactory = _serviceLocator.RegisterSingleInstance<IUIFactory>(new UIFactory(
                assetProvider,
                staticData,
                progressService, 
                adService,
                wordService
            ));

            IWindowService windowService = _serviceLocator.RegisterSingleInstance<IWindowService>(new WindowService(uiFactory));
            uiFactory.Construct(windowService);
            
            IGameFactory gameFactory = _serviceLocator.RegisterSingleInstance<IGameFactory>(new GameFactory(
                assetProvider, 
                staticData, 
                progressService, 
                windowService
            ));
            
            _serviceLocator.RegisterSingleInstance<ISaveLoadService>(new SaveLoadService(
                progressService, 
                gameFactory
            ));
        }

        private IWordService RegisterWordService()
        {
            IWordService wordService = new WordService(new WordProvider());
            wordService.Init();
            return _serviceLocator.RegisterSingleInstance<IWordService>(wordService);
        }

        private IAdService RegisterAdService()
        {
            var adService = new AdService();
            adService.Init();
            return _serviceLocator.RegisterSingleInstance<IAdService>(adService);
        }

        private IStaticDataService RegisterStaticDataService()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadResources();
            return _serviceLocator.RegisterSingleInstance<IStaticDataService>(staticData);
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