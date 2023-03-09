using CodeBase.Data;
using CodeBase.Services.Progress;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public abstract class BaseWindow : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        protected IStorageProgressService progressService;
        protected PlayerProgress Progress => progressService.Progress;

        public void Construct(IStorageProgressService progressService) =>
            this.progressService = progressService;

        private void Awake() => 
            OnAwake();

        protected virtual void OnAwake() =>
            _closeButton.onClick.AddListener(() => Destroy(gameObject));

        private void Start()
        {
            Init();
            SubscribeUpdates();
        }

        protected virtual void Init() {}
        protected virtual void SubscribeUpdates() {}
        protected virtual void UnSubscribeUpdates() {}

        private void OnDestroy()
        {
            UnSubscribeUpdates();
        }
    }
}