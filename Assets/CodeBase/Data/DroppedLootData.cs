using System;

namespace CodeBase.Data
{
    [Serializable]
    public class DroppedLootData
    {
        public PositionOnLevel positionOnLevel;
        public Loot loot;
        public string id;

        public DroppedLootData(string id, Loot loot, string levelName, Vector3Data position)
        {
            this.loot = loot;
            this.id = id;
            positionOnLevel = new PositionOnLevel(levelName, position);
        }
    }
}