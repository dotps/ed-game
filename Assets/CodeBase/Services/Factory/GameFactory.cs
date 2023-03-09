using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Enemies;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Logic;
using CodeBase.Logic.EnemySpawners;
using CodeBase.Services.Progress;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace CodeBase.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        private GameObject HeroGameObject { get; set; }

        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IStorageProgressService _progressService;
        private readonly IWindowService _windowService;

        public GameFactory(IAssetProvider assets, IStaticDataService staticData, IStorageProgressService progressService, IWindowService windowService)
        {
            _assets = assets;
            _staticData = staticData;
            _progressService = progressService;
            _windowService = windowService;
        }

        public GameObject CreateHero(Vector3 placeAt)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPath, placeAt);
            return HeroGameObject;
        }

        public GameObject CreateHUD()
        {
            GameObject hud = InstantiateRegistered(AssetPath.HUDPath);
            hud.GetComponentInChildren<LootObserver>().Construct(_progressService.Progress.worldData);
            
            foreach (OpenWindowButton button in hud.GetComponentsInChildren<OpenWindowButton>())
                button.Construct(_windowService);

            return hud;
        }

        public GameObject CreateMonster(MonsterTypeId monsterTypeId, Transform parent)
        {
            MonsterStaticData monsterStaticData = _staticData.GetMonsterData(monsterTypeId);
            GameObject monster = Object.Instantiate(monsterStaticData.prefab, parent.position, Quaternion.identity, parent);

            IHealth health = monster.GetComponent<IHealth>();
            health.CurrentHp = monsterStaticData.hp;
            health.MaxHp = monsterStaticData.hp;
            
            monster.GetComponent<ActorUI>().Construct(health);
            monster.GetComponent<AgentMoveToPlayer>().Construct(HeroGameObject.transform);
            monster.GetComponent<NavMeshAgent>().speed = monsterStaticData.moveSpeed;
            monster.GetComponent<RotateToHero>()?.Construct(HeroGameObject.transform);

            Attack attack = monster.GetComponent<Attack>();
            attack.Construct(HeroGameObject.transform);
            attack.damage = monsterStaticData.damage;
            attack.attackCooldown = monsterStaticData.attackCooldown;
            attack.attackDistance = monsterStaticData.attackDistance;
            attack.attackRadius = monsterStaticData.attackRadius;

            LootSpawner lootSpawner = monster.GetComponentInChildren<LootSpawner>();
            lootSpawner.SetLoot(monsterStaticData.minLoot, monsterStaticData.maxLoot);
            if (lootSpawner != null) 
                lootSpawner.Construct(this);

            return monster;
        }

        public LootInstance CreateLoot()
        {
            LootInstance lootInstance = InstantiateRegistered(AssetPath.LootPath).GetComponent<LootInstance>();
            lootInstance.Construct(_progressService.Progress.worldData);
            
            return lootInstance;
        }

        public void CreateDroppedLoot()
        {
            foreach (var droppedLoot in _progressService.Progress.droppedLoot.dropped)
            {
                LootInstance lootInstance = CreateLoot();
                lootInstance.transform.position = droppedLoot.positionOnLevel.position.AsVector3();

                Loot lootValue = new Loot() { value = droppedLoot.loot.value };
                lootInstance.Init(lootValue, droppedLoot.id);
            }
        }

        public void CreateSpawner(Vector3 position, string spawnerId, MonsterTypeId monsterTypeId)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetPath.SpawnerPath, position).GetComponent<SpawnPoint>();
            spawner.Construct(this);
            spawner.monsterTypeId = monsterTypeId;
            spawner.Id = spawnerId;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (var progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject gameObject = _assets.LoadAndInstantiate(prefabPath, position);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.LoadAndInstantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        public void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);
            ProgressReaders.Add(progressReader);
        }
    }
}