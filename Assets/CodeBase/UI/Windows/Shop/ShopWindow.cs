using CodeBase.Services.Ad;
using CodeBase.Services.Progress;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
    public class ShopWindow : BaseWindow
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private RewardedAdItem _adItem;

        public void Construct(IAdService adService, IProgressService progressService)
        {
            base.Construct(progressService);
            _adItem.Construct(adService, progressService);
        }
        
        protected override void Init()
        {
            base.Init();
            _adItem.Init();
            UpdateMoneyText();
        }

        private void UpdateMoneyText() =>
            _moneyText.text = Progress.worldData.collectedLoot.collected.ToString();

        protected override void SubscribeUpdates()
        {
            _adItem.Subscribe();
            Progress.worldData.collectedLoot.Changed += UpdateMoneyText;
        }

        protected override void UnSubscribeUpdates()
        {
            base.UnSubscribeUpdates();
            _adItem.Unsubscribe();
            Progress.worldData.collectedLoot.Changed -= UpdateMoneyText;
        }
    }
}