using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services;
using CodeBase.Services.Ad;
using CodeBase.Services.Progress;
using CodeBase.Services.Words;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Main;
using CodeBase.UI.Windows.Shop;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IProgressService _progressService;
        private readonly IAdService _adService;
        private readonly IWordService _wordService;
        private IWindowService _windowService;

        private Transform _ui;

        public UIFactory(IAssetProvider assets, IStaticDataService staticData, IProgressService progressService, IAdService adService, IWordService wordService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _adService = adService;
            _wordService = wordService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.GetWindowConfig(WindowId.Shop);
            ShopWindow window = Object.Instantiate(config.prefab, _ui) as ShopWindow;
            window.Construct(_adService, _progressService);
        }

        public void CreateUIContainer() =>
            _ui = _assets.LoadAndInstantiate(AssetPath.UIPath).transform;

        public void CreateMainWindow()
        {
            WindowConfig config = _staticData.GetWindowConfig(WindowId.Main);
            MainWindow window = Object.Instantiate(config.prefab, _ui) as MainWindow;
            window.Construct(_progressService);
            
            InitOpenWindowButton(window);
        }

        public void CreateWordsWindow()
        {
            WindowConfig config = _staticData.GetWindowConfig(WindowId.Words);
            WordsWindow window = Object.Instantiate(config.prefab, _ui) as WordsWindow;
            window.Construct(_progressService, _wordService);
            
            InitOpenWindowButton(window);
        }

        public void CreateAddWordWindow()
        {
            WindowConfig config = _staticData.GetWindowConfig(WindowId.AddWord);
            AddWordWindow window = Object.Instantiate(config.prefab, _ui) as AddWordWindow;
            window.Construct(_progressService, _wordService);
            
            InitOpenWindowButton(window);
        }

        private void InitOpenWindowButton(BaseWindow window)
        {
            foreach (OpenWindowButton button in window.GetComponentsInChildren<OpenWindowButton>())
                button.Construct(_windowService);
        }

        public void Construct(IWindowService windowService) => 
            _windowService = windowService;
    }
}