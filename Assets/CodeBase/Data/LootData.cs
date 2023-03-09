using System;
using CodeBase.Services.Progress;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class LootData
    {
        public int collected;
        public Action Changed;

        public void Collect(Loot loot)
        {
            collected += loot.value;
            Changed?.Invoke();
        }
    }
}