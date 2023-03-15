using System;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Data.Words;
using CodeBase.Infrastructure.API;
using CodeBase.Services.Translate;
using UnityEngine;

namespace CodeBase.Services.Words
{
    public class WordService : IWordService
    {
        private readonly ITranslateService _translateService;
        private readonly WordProvider _wordProvider;

        public WordService(WordProvider wordProvider, ITranslateService translateService)
        {
            _translateService = translateService;
            _wordProvider = new WordProvider();
        }

        public bool TryAdd(WordData word)
        {
            if (_wordProvider.Words.TryAdd(word.word, word))
            {
                Debug.Log("ADDED");
                Debug.Log(_wordProvider.Words.Count);
                _wordProvider.Save();
                return true;
            }
            else 
                return false;
        }

        public void SubscribeUpdates()
        {
            _wordProvider.Changed += RefreshWords;
        }
        
        public void UnSubscribeUpdates()
        {
            _wordProvider.Changed -= RefreshWords;
        }

        private void RefreshWords()
        {
            Debug.Log("RefreshWords");
        }

        public List<WordData> GetWords()
        {
            return null;
        }
    }

}