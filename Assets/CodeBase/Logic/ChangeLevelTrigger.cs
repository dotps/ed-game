using CodeBase.Infrastructure.StateMachine;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Logic
{
    public class ChangeLevelTrigger : MonoBehaviour
    {
        private const string PlayerTag = "Player";
        
        [SerializeField] private string _transferToLevel;
        
        private IGameStateMachine _stateMachine;
        private bool _isTriggered;

        private void Awake()
        {
            _stateMachine = ServiceLocator.Instance.GetSingleInstance<IGameStateMachine>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isTriggered) return;
            if (!other.CompareTag(PlayerTag)) return;

            _isTriggered = true;
            _stateMachine.Enter<LoadLevelState, string>(_transferToLevel);
        }
    }
}