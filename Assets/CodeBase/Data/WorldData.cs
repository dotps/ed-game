using System;
using UnityEngine.Serialization;

namespace CodeBase.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel positionOnLevel;
        [FormerlySerializedAs("lootData")] public LootData collectedLoot;

        public WorldData(string levelName)
        {
            positionOnLevel = new PositionOnLevel(levelName);
            collectedLoot = new LootData();
        }
    }
}