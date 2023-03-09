using System;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Shop;

namespace CodeBase.StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId windowId;
        public BaseWindow prefab;
    }
}