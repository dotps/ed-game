using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear Player Prefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
