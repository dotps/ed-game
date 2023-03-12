using CodeBase.Services.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Main
{
    public class AddWordWindow : BaseWindow
    {
        [SerializeField] private Button _addWordButton;
        [SerializeField] private TMP_InputField _word;
        [SerializeField] private TMP_InputField _wordTraslation;
        [SerializeField] private TMP_InputField _wordTranscription;
        [SerializeField] private TMP_InputField _wordTextSpeech;
        
        public void Construct(IProgressService progressService)
        {
            base.Construct(progressService);
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            _addWordButton.onClick.AddListener(SaveWord);
        }

        private void SaveWord()
        {
            Debug.Log(_word.text);
        }

        protected override void SubscribeUpdates()
        {
            // Progress.worldData.collectedLoot.Changed += UpdateTitleText;
        }

        protected override void UnSubscribeUpdates()
        {
            base.UnSubscribeUpdates();
            // Progress.worldData.collectedLoot.Changed -= UpdateTitleText;
        }
    }
}