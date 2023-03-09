using CodeBase.Data;
using CodeBase.Enemies;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.Input;
using CodeBase.Services.Progress;
using UnityEngine;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class HeroAttack : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private CharacterController _characterController;

        private IInputService _input;
        private static int _layerMask;

        private Collider[] _hits = new Collider[3];

        private Stats _heroStats;

        private void Awake()
        {
            _input = ServiceLocator.Instance.GetSingleInstance<IInputService>();
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }
        
        private Vector3 StartPoint() => 
            new Vector3(transform.position.x, _characterController.center.y / 2, transform.position.z);

        
        private void Update()
        {
            if (_input.IsAttackButtonUp() && !_animator.IsAttacking)
                _animator.PlayAttack();
        }

        private void OnAttack()
        {
            int hitsCount = GetHits();
            for (int i = 0; i < hitsCount; i++)
            {
                _hits[i].transform.parent.GetComponent<IHealth>().TakeDamage(_heroStats.damage);
            }
        }
        
        private int GetHits() => 
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _heroStats.damageRadius, _hits, _layerMask);

        
        public void LoadProgress(PlayerProgress progress) => 
            _heroStats = progress.heroStats;

        public void UpdateProgress(PlayerProgress progress)
        {
        }
    }
}