using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]
    public class AnimatedAlongAgent : MonoBehaviour
    {
        private const float MinimalVelocity = 0.1f;
        
        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private EnemyAnimator _animator;

        private void Update()
        {
            if (CanMove())
                _animator.Move(_navMeshAgent.velocity.magnitude);
            else
            {
                _animator.StopMoving();
            }
        }

        private bool CanMove() =>
            _navMeshAgent.velocity.magnitude > MinimalVelocity && _navMeshAgent.remainingDistance > _navMeshAgent.radius;
    }
}