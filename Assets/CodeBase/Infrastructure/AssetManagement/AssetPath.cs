using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public static class AssetPath
    {
        public const string HeroPath = "Hero/hero";
        public const string LootPath = "Loot/Loot";
        public const string SpawnerPath = "Enemies/SpawnPoint";
        
        public const string HUDPath = "Hud/Hud";
        public const string UIPath = "UI/UI";
        
        public const string DynamicDataPath = "/DynamicData/";
        public static readonly string StoragePath = Application.isEditor ? Application.dataPath + "/Resources" + DynamicDataPath : Application.persistentDataPath + DynamicDataPath;
        public static readonly string WordsDataPath = StoragePath + "words.json";
    }
}