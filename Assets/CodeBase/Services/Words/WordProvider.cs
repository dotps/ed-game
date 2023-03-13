using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeBase.Data;
using CodeBase.Data.Words;
using UnityEngine;

namespace CodeBase.Services.Words
{
    public class WordProvider
    {
        public const string DynamicDataPath = "/DynamicData/";
        public static readonly string StoragePath = Application.isEditor ? Application.dataPath + "/Resources" + DynamicDataPath : Application.persistentDataPath + DynamicDataPath;
        public static readonly string WordsDataPath = StoragePath + "words.json";

        public event Action Changed;
        
        private WordService _wordService;

        public Dictionary<string, WordData> Words { get; private set; }

        public WordProvider()
        {
            Words = new Dictionary<string, WordData>();
            Load();
        }

        private void Load()
        {
            if (!File.Exists(WordsDataPath)) return;
            
            Words = File.ReadAllText(WordsDataPath)
                .ToDeserialized<WordsList>().items
                .ToDictionary(x => x.word, x => x);
        }

        public void Save()
        {
            var words = new WordsList
            {
                items = Words.Values.ToList()
            };
            
            File.WriteAllText(WordsDataPath, words.ToJson());
            Changed?.Invoke();
        }
    }
}