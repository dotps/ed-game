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
    public class WordsWindow : BaseWindow
    {
        public void Construct(IProgressService progressService)
        {
            base.Construct(progressService);
        }

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