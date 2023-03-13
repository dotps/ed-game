using CodeBase.Data;
using CodeBase.Services.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows
{
    public abstract class BaseWindow : MonoBehaviour
    {
        [SerializeField] private string _title;
        [SerializeField] private TMP_Text _titleGameObject;
        [SerializeField] private Button _closeButton;
        protected IProgressService progressService;
        protected PlayerProgress Progress => progressService.Progress;

        public void Construct(IProgressService progressService) =>
            this.progressService = progressService;

        private void Awake() => 
            OnAwake();

        protected virtual void OnAwake()
        {
            if (_closeButton != null)
                _closeButton.onClick.AddListener(Hide);
        }

        protected void Hide()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            Init();
            SubscribeUpdates();
        }

        protected virtual void Init()
        {
            UpdateTitleText();
        }
        protected virtual void SubscribeUpdates() {}
        protected virtual void UnSubscribeUpdates() {}
        
        private void UpdateTitleText() =>
            _titleGameObject.text = _title;

        private void OnDestroy()
        {
            UnSubscribeUpdates();
        }
    }
}