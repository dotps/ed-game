using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.Ad;
using CodeBase.Services.Progress;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Shop;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IStorageProgressService _progressService;
        private readonly IAdService _adService;
        
        private Transform _ui;

        public UIFactory(IAssetProvider assets, IStaticDataService staticData, IStorageProgressService progressService, IAdService adService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _adService = adService;
        }

        public void CreateShop()
        {
            WindowConfig config = _staticData.GetWindowConfig(WindowId.Shop);
            ShopWindow window = Object.Instantiate(config.prefab, _ui) as ShopWindow;
            window.Construct(_adService, _progressService);
        }

        public void CreateUIContainer() =>
            _ui = _assets.LoadAndInstantiate(AssetPath.UIPath).transform;
    }
}