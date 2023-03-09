using System;
using CodeBase.Data;
using CodeBase.Services.Factory;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Enemies
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        
        private IGameFactory _gameFactory;
        private int _lootMin;
        private int _lootMax;

        public void Construct(IGameFactory gameFactory) => 
            _gameFactory = gameFactory;

        private void Start() =>
            _enemyDeath.EnemyDied += SpawnLoot;

        private void SpawnLoot()
        {
            LootInstance loot = _gameFactory.CreateLoot();
            loot.transform.position = transform.position;

            Loot lootItem = GenerateLoot();
            loot.Init(lootItem);
        }

        private Loot GenerateLoot()
        {
            return new Loot()
            {
                value = Random.Range(_lootMin, _lootMax)
            };
        }

        public void SetLoot(int min, int max)
        {
            _lootMin = min;
            _lootMax = max;
        }
        

        private void OnDestroy() => 
            _enemyDeath.EnemyDied -= SpawnLoot;

        
    }
}