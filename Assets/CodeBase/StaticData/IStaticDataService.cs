using CodeBase.Services;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.Windows;

namespace CodeBase.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadResources();
        MonsterStaticData GetMonsterData(MonsterTypeId monsterTypeId);
        LevelStaticData GetLevelData(string sceneKey);
        WindowConfig GetWindowConfig(WindowId windowId);
    }
}