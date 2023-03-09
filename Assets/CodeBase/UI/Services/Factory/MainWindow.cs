using CodeBase.Services.Ad;
using CodeBase.Services.Progress;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Shop;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public class MainWindow : BaseWindow
    {
        [SerializeField] private TMP_Text _moneyText;

        public void Construct(IAdService adService, IProgressService progressService)
        {
            base.Construct(progressService);
        }
        
        protected override void Init()
        {
            UpdateMoneyText();
        }

        private void UpdateMoneyText() =>
            _moneyText.text = Progress.worldData.collectedLoot.collected.ToString();

        protected override void SubscribeUpdates()
        {
            Progress.worldData.collectedLoot.Changed += UpdateMoneyText;
        }

        protected override void UnSubscribeUpdates()
        {
            base.UnSubscribeUpdates();
            Progress.worldData.collectedLoot.Changed -= UpdateMoneyText;
        }
    }
}