using CodeBase.Data;
using CodeBase.Infrastructure.API;
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
using UnityEngine.Networking;

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

        private async void RegisterServices()
        {
            // StartCoroutine(GetRequest("https://jsonplaceholder.typicode.com/posts", (UnityWebRequest req) =>
            // {
            //     if (req.isNetworkError || req.isHttpError)
            //     {
            //         Debug.Log($"{req.error}: {req.downloadHandler.text}");
            //     } else
            //     {
            //         Post[] posts = JsonConvert.DeserializeObject<Post[]>(req.downloadHandler.text);
            //
            //         foreach(Post post in posts)
            //         {
            //             Debug.Log(post.title);
            //         }
            //     }
            // }));

            // var request;
            // Api.Get("https://www.teploprofi.com", (UnityWebRequest req) =>
            // {
            //     if (req.isNetworkError || req.isHttpError)
            //         Debug.Log($"{req.error}: {req.downloadHandler.text}");
            //     else
            //     {
            //         Debug.Log("++++");
            //         // Post[] posts = JsonConvert.DeserializeObject<Post[]>(req.downloadHandler.text);
            //         //
            //         // foreach(Post post in posts)
            //         // {
            //         //     Debug.Log(post.title);
            //         // }
            //     }
            // });

            // var api = new Api();
            // var result = await api.Get<LootData>("https://www.teploprofi.com");
            // var result = await api.Get<User>("https://jsonplaceholder.typicode.com/todos/1");
            
            // Debug.Log(result);
            // Debug.Log(result.Title);
            
            // Debug.Log(api.Get<LootData>("https://www.teploprofi.com").Result);
            
            // UnityWebRequestAsyncOperation asyncOperation;
            // asyncOperation = UnityWebRequest.Get("myurl.com").SendWebRequest();
            
            IStaticDataService staticData = RegisterStaticDataService();
            IAdService adService = RegisterAdService();
            
            _serviceLocator.RegisterSingleInstance<IGameStateMachine>(_stateMachine);
            _serviceLocator.RegisterSingleInstance<IInputService>(GetInputService());
            
            IAssetProvider assetProvider = _serviceLocator.RegisterSingleInstance<IAssetProvider>(new AssetProvider());
            IProgressService progressService = _serviceLocator.RegisterSingleInstance<IProgressService>(new ProgressService());
            
            IWordService wordService = _serviceLocator.RegisterSingleInstance<IWordService>(new WordService(new WordProvider()));
                
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
    
    // public class User
    // {
    //     public int UserId { get; set; }
    //     public int Id { get; set; }
    //     public string Title { get; set; }
    //     public bool Completed { get; set; }
    // }
}