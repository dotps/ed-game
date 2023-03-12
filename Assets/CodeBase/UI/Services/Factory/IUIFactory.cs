using CodeBase.Services;
using CodeBase.UI.Services.Windows;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void Construct(IWindowService windowService);
        
        void CreateShop();
        void CreateUIContainer();
        void CreateMainWindow();
        void CreateWordsWindow();
        void CreateAddWordWindow();
    }
}