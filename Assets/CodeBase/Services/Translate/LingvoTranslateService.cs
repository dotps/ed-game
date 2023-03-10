using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.API;
using Newtonsoft.Json;
using UnityEngine;

namespace CodeBase.Services.Translate
{
    /* Json Serilization
     * Используется пакет для серилизации json com.unity.nuget.newtonsoft-json (официально поддерживаается unity)
     * https://github.com/jilleJr/Newtonsoft.Json-for-Unity/wiki/Install-official-via-UPM
     */
    
    class LingvoTranslateService : Api, ITranslateService
    {
        private const string ApiKey = "ZGViYzU4NjUtMTMxNy00YWI3LWI4Y2ItZDZjYTdiM2EzZDk2OmNkODM2NWVkNTNkNzQ1Mjc5YWMxY2Y2NGRmNTYyYjdj";
        private const string Token = "ZXlKaGJHY2lPaUpJVXpJMU5pSXNJblI1Y0NJNklrcFhWQ0o5LmV5SmxlSEFpT2pFMk56ZzROamt5TXpnc0lrMXZaR1ZzSWpwN0lrTm9ZWEpoWTNSbGNuTlFaWEpFWVhraU9qVXdNREF3TENKVmMyVnlTV1FpT2pnMU56a3NJbFZ1YVhGMVpVbGtJam9pWkdWaVl6VTROalV0TVRNeE55MDBZV0kzTFdJNFkySXRaRFpqWVRkaU0yRXpaRGsySW4xOS5lN0lFWnVpQzlhTTQ3WjY3SnpTQW0xOU5LdW9sZ2ZJV3BwSzA4eTZBRV9z";
        private const string Host = "https://developers.lingvolive.com/api/";
        private const string CmdAuth = "v1.1/authenticate";
        
        /*
         * TODO: Пока работает с указанием токена в конcтанте, но нужно запрашивать токен 
         */
        // public async void GetToken()
        // {
        //     // string token = await GetRequest<string>(Host + CmdAuth);
        // }
        
        public async Task<TResultType> Get<TResultType>(string url)
        {
            string result = await GetRequest(url, Token);

            Debug.Log(result);
            
            if (String.IsNullOrEmpty(result))
            {
                Debug.Log("++++++++++++++");
                return default(TResultType);
            }

            try
            {
                return JsonConvert.DeserializeObject<TResultType>(result);
            }
            catch 
            {
                string json = JsonArrayToObject<TResultType>(result);
                return JsonConvert.DeserializeObject<TResultType>(json);
            }
        }

        private static string JsonArrayToObject<TResultType>(string result)
        {
            return "{'" + typeof(TResultType).Name + "':" + result + "}";
        }
    }

    [Serializable]
    public class LingvoTranslate
    {
        public List<Translate> lingvoTranslate { get; set; }
    }

    [Serializable]
    public class Body
    {
        public List<Markup> Markup { get; set; }
        public string Node { get; set; }
        public object Text { get; set; }
        public bool IsOptional { get; set; }
        public int? Type { get; set; }
        public List<Item> Items { get; set; }
    }

    [Serializable]
    public class Item
    {
        public List<Markup> Markup { get; set; }
        public string Node { get; set; }
        public object Text { get; set; }
        public bool IsOptional { get; set; }
    }

    [Serializable]
    public class Markup
    {
        public string Node { get; set; }
        public string Text { get; set; }
        public bool IsOptional { get; set; }
        public bool? IsItalics { get; set; }
        public bool? IsAccent { get; set; }
        public string FullText { get; set; }
        public string FileName { get; set; }
        public List<Markup> markup { get; set; }
        public string Dictionary { get; set; }
        public string ArticleId { get; set; }
        public int? Type { get; set; }
        public List<Item> Items { get; set; }
    }

    [Serializable]
    public class TitleMarkup
    {
        public bool IsItalics { get; set; }
        public bool IsAccent { get; set; }
        public string Node { get; set; }
        public string Text { get; set; }
        public bool IsOptional { get; set; }
    }

    [Serializable]
    public class Translate
    {
        public string Title { get; set; }
        public List<TitleMarkup> TitleMarkup { get; set; }
        public string Dictionary { get; set; }
        public string ArticleId { get; set; }
        public List<Body> Body { get; set; }
    }
    
    [Serializable]
    public class LingvoMinicard
    {
        public int SourceLanguage { get; set; }
        public int TargetLanguage { get; set; }
        public string Heading { get; set; }
        public Translation Translation { get; set; }
        public List<object> SeeAlso { get; set; }
    }

    [Serializable]
    public class Translation
    {
        public string Heading { get; set; }
        public string translation { get; set; }
        public string DictionaryName { get; set; }
        public string SoundName { get; set; }
        public int Type { get; set; }
        public string OriginalWord { get; set; }
    }
    
}