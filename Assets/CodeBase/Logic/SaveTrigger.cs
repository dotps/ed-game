using CodeBase.Services;
using CodeBase.Services.SaveLoad;
using UnityEngine;

namespace CodeBase.Logic
{
    public class SaveTrigger : MonoBehaviour
    {
        private ISaveLoadService _saveLoadService;

        [SerializeField] private BoxCollider _collider;

        private void Awake()
        {
            _saveLoadService = ServiceLocator.Instance.GetSingleInstance<ISaveLoadService>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _saveLoadService.SaveProgress();
            Debug.Log("Progress saved");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            if (!_collider) 
                return;
            
            Gizmos.color = new Color(30, 200, 30, 13);
            Gizmos.DrawCube(transform.position + _collider.center, _collider.size);
        }
    }
}