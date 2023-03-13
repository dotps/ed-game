using System;
using System.Collections.Generic;
using CodeBase.Data.Words;

namespace CodeBase.Services.Words
{
    public class WordService : IWordService
    {
        public WordService()
        {
            
        }
        
        public Dictionary<string, WordData> Load()
        {
            // var prefab = Resources.Load<GameObject>(path);
            // return Object.Instantiate(prefab);
            
            return null;
        }
            
        public void Save(string data)
        {
            if (String.IsNullOrEmpty(data))
                return;
            
            // File.WriteAllText(AssetPath.WordsDataPath, data);
        }
    }

}