using System.Collections.Generic;
using System.Linq;
using CodeBase.Services;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;
using UnityEngine;

namespace CodeBase.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataMonstersPath = "StaticData/Monsters";
        private const string StaticDataLevelsPath = "StaticData/Levels";
        private const string StaticDataWindowsPath = "StaticData/UI/WindowsData";
        
        private Dictionary<MonsterTypeId, MonsterStaticData> _monsters;
        private Dictionary<string, LevelStaticData> _levels;
        private Dictionary<WindowId, WindowConfig> _windowsConfigs;

        public void LoadResources()
        {
            _monsters = Resources.LoadAll<MonsterStaticData>(StaticDataMonstersPath)
                .ToDictionary(x => x.monsterTypeId, x => x);
            _levels = Resources.LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.levelKey, x => x);
            _windowsConfigs = Resources.Load<WindowStaticData>(StaticDataWindowsPath)
                .configs.ToDictionary(x => x.windowId, x => x);
        }

        public MonsterStaticData GetMonsterData(MonsterTypeId monsterTypeId) => 
            _monsters.TryGetValue(monsterTypeId, out MonsterStaticData staticData) ? staticData : null;

        public LevelStaticData GetLevelData(string sceneKey) => 
            _levels.TryGetValue(sceneKey, out LevelStaticData staticData) ? staticData : null;

        public WindowConfig GetWindowConfig(WindowId windowId) =>
            _windowsConfigs.TryGetValue(windowId, out WindowConfig config) ? config : null;
    }
}