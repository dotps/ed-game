using CodeBase.Services.Ad;
using CodeBase.Services.Progress;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Main
{
    public class MainWindow : BaseWindow
    {
        [SerializeField] private string _title;
        [SerializeField] private TMP_Text _titleGameObject;
        [SerializeField] private Button _worldsButton;
        
        private UIFactory _uiFactory;

        public void Construct(IProgressService progressService, UIFactory uiFactory)
        {
            base.Construct(progressService);
            _uiFactory = uiFactory;
        }
        
        protected override void Init()
        {
            UpdateTitleText();
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            // _worldsButton.onClick.AddListener(OpenWordsWindow);
        }

        private void OpenWordsWindow() =>
            _uiFactory.CreateMainWords();

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