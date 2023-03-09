using CodeBase.Data;
using CodeBase.Services.Ad;
using CodeBase.Services.Progress;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
    public class RewardedAdItem : MonoBehaviour
    {
        [SerializeField] private Button _buttonShowAd;
        [SerializeField] private GameObject[] _activeAdObjects;
        [SerializeField] private GameObject[] _inactiveAdObjects;
        
        private IAdService _adService;
        private IProgressService _progressService;

        public void Construct(IAdService adService, IProgressService progressService)
        {
            _adService = adService;
            _progressService = progressService;
        }

        public void Init()
        {
            _buttonShowAd.onClick.AddListener(OnShowAdButtonClicked);
            RefreshAvailableAd();
        }

        public void Subscribe() => 
            _adService.RewardedVideoReady += RefreshAvailableAd;

        public void Unsubscribe() => 
            _adService.RewardedVideoReady -= RefreshAvailableAd;

        private void RefreshAvailableAd()
        {
            bool isVideoReady = _adService.IsRewardedVideoReady;
            
            foreach (GameObject activeAdObject in _activeAdObjects) 
                activeAdObject.SetActive(isVideoReady);
            foreach (GameObject inactiveAdObject in _inactiveAdObjects) 
                inactiveAdObject.SetActive(!isVideoReady);
        }

        private void OnShowAdButtonClicked() => 
            _adService.ShowRewardedVideo(OnVideoFinished);

        private void OnVideoFinished()
        {
            Loot loot = new Loot() { value = _adService.RewardValue };
            _progressService.Progress.worldData.collectedLoot.Collect(loot);
        }
    }
}