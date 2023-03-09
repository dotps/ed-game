using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Enemies
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private Follow _followToPlayer;
        [SerializeField] private float _cooldownSeconds;
        private WaitForSeconds _waitCooldownSeconds;
        private Coroutine _aggroCoroutine;
        private bool _hasTarget;

        private void Start()
        {
            _triggerObserver.TriggerEnter += OnEnterTrigger;
            _triggerObserver.TriggerExit += OnExitTrigger;
            SetFollowActive(false);
            _waitCooldownSeconds = new WaitForSeconds(_cooldownSeconds);
        }

        private void OnEnterTrigger(Collider obj)
        {
            if (_hasTarget) return;
            
            StopAggroCoroutine();
            SetFollowActive(true);
            _hasTarget = true;
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private void OnExitTrigger(Collider obj)
        {
            if (!_hasTarget) return;

            _aggroCoroutine = StartCoroutine(SetFollowOffAfterCooldown());
            _hasTarget = false;
        }

        private IEnumerator SetFollowOffAfterCooldown()
        {
            yield return _waitCooldownSeconds;
            SetFollowActive(false);
        }

        private bool SetFollowActive(bool value) =>
            _followToPlayer.enabled = value;

        private void OnDestroy()
        {
            _triggerObserver.TriggerEnter -= OnEnterTrigger;
            _triggerObserver.TriggerExit -= OnExitTrigger;
        }
    }
}