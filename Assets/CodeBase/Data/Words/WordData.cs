using System;

namespace CodeBase.Data.Words
{
    [Serializable]
    public class WordData
    {
        public string word;
        public string wordTranslation;
        public string wordTranscription;
        public string wordTextSpeech;

        public WordData(string word, string wordTranslation, string wordTranscription, string wordTextSpeech)
        {
            this.word = word;
            this.wordTranslation = wordTranslation;
            this.wordTranscription = wordTranscription;
            this.wordTextSpeech = wordTextSpeech;
        }
    }
}