using System;
using UnityEngine.Serialization;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public HeroState heroState;
        public Stats heroStats;
        public WorldData worldData;
        public KillData killData;
        [FormerlySerializedAs("droppedLootData")] public DroppedLoot droppedLoot;

        public PlayerProgress(string levelName)
        {
            worldData = new WorldData(levelName);
            heroState = new HeroState();
            heroStats = new Stats();
            killData = new KillData();
            droppedLoot = new DroppedLoot();
        }
    }
}