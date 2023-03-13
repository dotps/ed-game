using System;
using CodeBase.Data.Words;
using CodeBase.Services.Progress;
using CodeBase.Services.Words;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Main
{
    public class AddWordWindow : BaseWindow
    {
        [SerializeField] private Button _addWordButton;
        [SerializeField] private TMP_InputField _word;
        [SerializeField] private TMP_InputField _wordTranslation;
        [SerializeField] private TMP_InputField _wordTranscription;
        [SerializeField] private TMP_InputField _wordTextSpeech;
        
        private IWordService _wordService;

        public void Construct(IProgressService progressService, IWordService wordService)
        {
            base.Construct(progressService);
            _wordService = wordService;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            _addWordButton.onClick.AddListener(AddWord);
        }

        private void AddWord()
        {
            if (String.IsNullOrEmpty(_word.text)) 
                return;
            
            WordData word = new WordData(
                _word.text,
                _wordTranslation.text,
                _wordTranscription.text,
                _wordTextSpeech.text
            );

            if (_wordService.TryAdd(word))
            {
                //
            }
            else
            {
                // Popup then word exist, 
            }

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