using CodeBase.Services.Progress;
using CodeBase.UI.Windows;

namespace CodeBase.UI.Services.Factory
{
    public class AddWordWindow : BaseWindow
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