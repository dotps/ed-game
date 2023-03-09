using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemies
{
    [RequireComponent(typeof(EnemyHealth), typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private GameObject _deathFX;
        
        private float _secondsBeforeDestroy = 3f;
        private WaitForSeconds _waitForSecondsBeforeDestroy;

        public event Action EnemyDied;

        private void Start()
        {
            _health.HealthChanged += OnHealthChanged;
            _waitForSecondsBeforeDestroy = new WaitForSeconds(_secondsBeforeDestroy);
        }

        private void OnHealthChanged()
        {
            if (_health.CurrentHp <= 0)
                Die();
        }

        private void Die()
        {
            _health.HealthChanged -= OnHealthChanged;
            
            _animator.PlayDeath();
            SpawnDeathFX();
            StartCoroutine(DestroyByTime());

            EnemyDied?.Invoke();
        }

        private void SpawnDeathFX() => 
            Instantiate(_deathFX, transform.position, Quaternion.identity);

        private IEnumerator DestroyByTime()
        {
            yield return _waitForSecondsBeforeDestroy;
            Destroy(gameObject);
        }

        private void OnDestroy() => 
            _health.HealthChanged -= OnHealthChanged;
    }
}