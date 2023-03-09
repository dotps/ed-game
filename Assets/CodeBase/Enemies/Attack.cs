using System.Linq;
using CodeBase.Hero;
using CodeBase.Logic;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Enemies
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;

        public float attackCooldown = 3.0f;
        public float attackRadius = 0.5f;
        public float attackDistance = 0.5f;
        public float damage = 10f;

        private Transform _heroTransform;
        private bool _isAttacking;
        private Collider[] _hits = new Collider[1];
        private int _layerMask;
        private bool _attackIsActive;
        private float _currentAttackCooldown;

        public void Construct(Transform heroTransform) => 
            _heroTransform = heroTransform;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            UpdateCooldown();

            if (CanAttack())
                StartAttack();
        }

        private void OnAttackStarted()
        {
            if (TryGetHit(out Collider hit))
            {
                hit.transform.GetComponent<IHealth>().TakeDamage(damage);
            }
        }

        private void OnAttackEnded()
        {
            _currentAttackCooldown = attackCooldown;
            _isAttacking = false;
        }

        public void DisableAttack()
        {
            _attackIsActive = false;
        }

        public void EnableAttack()
        {
            _attackIsActive = true;
        }

        private bool CooldownIsUp()
        {
            return _currentAttackCooldown <= 0f;
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _currentAttackCooldown -= Time.deltaTime;
        }

        private bool TryGetHit(out Collider hit)
        {
            var hitsCount = Physics.OverlapSphereNonAlloc(StartPoint(), attackRadius, _hits, _layerMask);
            hit = _hits.FirstOrDefault();

            return hitsCount > 0;
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) +
                   transform.forward * attackDistance;
        }

        private bool CanAttack()
        {
            return _attackIsActive && !_isAttacking && CooldownIsUp();
        }

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();
            _isAttacking = true;
        }

    }
}