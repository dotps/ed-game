using CodeBase.Data;
using CodeBase.Enemies;
using CodeBase.Services.Factory;
using CodeBase.Services.Progress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Logic.EnemySpawners
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        // [field:SerializeField] public MonsterTypeId MonsterTypeId { get;  private set; }
        public MonsterTypeId monsterTypeId;
        public string Id { get; set; }

        [SerializeField] private bool _isDead;

        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;

        public void Construct(IGameFactory gameFactory) => 
            _gameFactory = gameFactory;

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.killData.clearedSpawners.Contains(Id))
                _isDead = true;
            else
                Spawn();
        }

        private void Spawn()
        {
            GameObject monster = _gameFactory.CreateMonster(monsterTypeId, transform);
            _enemyDeath = monster.GetComponent<EnemyDeath>();
            _enemyDeath.EnemyDied += OnEnemyDied;
        }

        private void OnEnemyDied()
        {
            _isDead = true;
            if (_enemyDeath != null) 
                _enemyDeath.EnemyDied -= OnEnemyDied;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (_isDead)
                progress.killData.clearedSpawners.Add(Id);
        }
    }
}