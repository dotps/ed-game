using CodeBase.Services.Ad;
using CodeBase.Services.Progress;
using CodeBase.Services.Words;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Main
{
    public class WordsWindow : BaseWindow
    {
        private IWordService _wordService;

        public void Construct(IProgressService progressService, IWordService wordService)
        {
            base.Construct(progressService);
            _wordService = wordService;
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            _wordService.SubscribeUpdates();
        }

        protected override void UnSubscribeUpdates()
        {
            base.UnSubscribeUpdates();
            _wordService.UnSubscribeUpdates();
        }
    }
}