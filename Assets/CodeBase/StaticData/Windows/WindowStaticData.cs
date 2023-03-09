using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowData", menuName = "StaticData/Window Static Data")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> configs;
    }
}