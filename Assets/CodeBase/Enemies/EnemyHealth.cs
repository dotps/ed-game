using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Enemies
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _currentHP;
        [SerializeField] private float _maxHP;

        public event Action HealthChanged;

        public float CurrentHp
        {
            get => _currentHP;
            set => _currentHP = value;
        }

        public float MaxHp
        {
            get => _maxHP;
            set => _maxHP = value;
        }

        public void TakeDamage(float damage)
        {
            CurrentHp -= damage;
            _animator.PlayHit();

            HealthChanged?.Invoke();
        }
    }
}