using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
    [Serializable]
    public class DroppedLoot
    {
        public List<DroppedLootData> dropped = new List<DroppedLootData>();
    }
}