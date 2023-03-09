using UnityEngine;

namespace CodeBase.Enemies
{
    [RequireComponent(typeof(Attack))]
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private Attack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.TriggerEnter += OnEnterTrigger;
            _triggerObserver.TriggerExit += OnExitTrigger;

            _attack.DisableAttack();
        }

        private void OnExitTrigger(Collider obj)
        {
            _attack.DisableAttack();
        }

        private void OnEnterTrigger(Collider obj)
        {
            _attack.EnableAttack();
        }

        private void OnDestroy()
        {
            _triggerObserver.TriggerEnter -= OnEnterTrigger;
            _triggerObserver.TriggerExit -= OnExitTrigger;
        }
    }
}