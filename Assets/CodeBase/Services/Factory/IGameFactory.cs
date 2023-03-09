using System.Collections.Generic;
using CodeBase.Enemies;
using CodeBase.Services.Progress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        GameObject CreateHero(Vector3 placeAt);
        GameObject CreateHUD();
        GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent);
        LootInstance CreateLoot();
        void CreateDroppedLoot();
        void CreateSpawner(Vector3 position, string spawnerId, MonsterTypeId monsterTypeId);

        void Cleanup();
    }
}
