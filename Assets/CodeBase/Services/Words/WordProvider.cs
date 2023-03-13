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
        
        private WordService _wordService;

        public Dictionary<string, WordData> Words { get; private set; }

        public void Init(WordService wordService)
        {
            _wordService = wordService;
            Words = new Dictionary<string, WordData>();
            
            Load();
        }

        private void Load() => 
            Words = Resources.Load<TextAsset>(WordsDataPath).text
                .ToDeserialized<Data.Words.Words>().items
                .ToDictionary(x => x.word, x => x);

        public void Save()
        {
            string json = Words.ToJson();
            Debug.Log(json);
            // File.WriteAllText(WordsDataPath, json);
        }
    }
}