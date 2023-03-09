using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroHealth))]
    public class HeroDeath : MonoBehaviour
    {
        [SerializeField] private HeroHealth _health;
        [SerializeField] private HeroMove _move;
        [SerializeField] private HeroAttack _attack;
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private GameObject _deathFX;
        private bool _isDead;

        private void Start() => 
            _health.HealthChanged += OnHealthChange;

        private void OnHealthChange()
        {
            if (!_isDead && _health.CurrentHp <= 0) 
                Die();
        }

        private void Die()
        {
            _isDead = true;
            
            _move.enabled = false;
            _attack.enabled = false;
            _animator.PlayDeath();

            SpawnDeathFX();
        }

        private void SpawnDeathFX() => 
            Instantiate(_deathFX, transform.position, Quaternion.identity);

        private void OnDestroy() => 
            _health.HealthChanged -= OnHealthChange;
    }
}