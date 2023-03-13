using System;
using System.Collections.Generic;
using CodeBase.Data.Words;

namespace CodeBase.Services.Words
{
    public class WordService : IWordService
    {
        private readonly WordProvider _wordProvider;
        
        // private Dictionary<string, WordData> _words = new Dictionary<string, WordData>();

        public WordService(WordProvider wordProvider)
        {
            _wordProvider = wordProvider;
        }

        public void Init()
        {
            _wordProvider.Init(this);
        }

        // public void Save(string data)
        // {
        //     if (String.IsNullOrEmpty(data))
        //         return;
        //     
        //     // File.WriteAllText(AssetPath.WordsDataPath, data);
        // }

        public Dictionary<string, WordData> Load()
        {
            return null;
        }

        public bool TryAdd(WordData word)
        {
            if (_wordProvider.Words.TryAdd(word.word, word))
            {
                _wordProvider.Save();
                return true;
            }
            else 
                return false;
        }

        public List<WordData> GetWords()
        {
            return null;
        }
    }

}