using CodeBase.Services.Factory;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemies
{
    public class AgentMoveToPlayer : Follow
    {
        private const float MinimalDistance = 1;
        
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyDeath _enemyDeath;
        
        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private bool _canMove = true;

        public void Construct(Transform heroTransform)
        {
            _heroTransform = heroTransform;
            _enemyDeath.EnemyDied += StopMove;
        }

        private void StopMove() =>
            _canMove = false;

        private void Update() => 
            SetDestinationForAgent();

        private void SetDestinationForAgent()
        {
            if (HeroInitialized() && HeroNotReached() && _canMove)
                _agent.destination = _heroTransform.position;
        }

        private bool HeroInitialized() => 
            _heroTransform != null;

        private bool HeroNotReached() => 
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= MinimalDistance;
    }
}