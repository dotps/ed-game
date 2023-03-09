using System;
using CodeBase.Data;
using CodeBase.Logic;
using CodeBase.Services.Progress;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        public event Action HealthChanged;

        [SerializeField] private HeroAnimator _animator;
        private HeroState _heroState;

        public float CurrentHp
        {
            get => _heroState.currenHp;
            set
            {
                if (_heroState.currenHp == value)
                    return;

                _heroState.currenHp = value;
                HealthChanged?.Invoke();
            }
        }

        public float MaxHp
        {
            get => _heroState.maxHp;
            set => _heroState.maxHp = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _heroState = progress.heroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.heroState.currenHp = CurrentHp;
            progress.heroState.maxHp = MaxHp;
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHp <= 0)
                return;

            CurrentHp -= damage;
            _animator.PlayHit();
        }
    }
}