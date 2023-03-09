using CodeBase.Services.Ad;
using CodeBase.Services.Progress;
using CodeBase.UI.Windows.Shop;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Main
{
    public class MainWindow : BaseWindow
    {
        [SerializeField] private string _title;
        [SerializeField] private TMP_Text _titleGameObject;

        public void Construct(IProgressService progressService)
        {
            base.Construct(progressService);
        }
        
        protected override void Init()
        {
            UpdateTitleText();
        }

        private void UpdateTitleText() =>
            _titleGameObject.text = _title;

        protected override void SubscribeUpdates()
        {
            // Progress.worldData.collectedLoot.Changed += UpdateTitleText;
        }

        protected override void UnSubscribeUpdates()
        {
            base.UnSubscribeUpdates();
            // Progress.worldData.collectedLoot.Changed -= UpdateTitleText;
        }
    }
}