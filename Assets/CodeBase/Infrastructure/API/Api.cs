using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Infrastructure.API
{
    // abstract
    public class Api
    {
        private const string ContentTypeJson = "application/json";

        public async Task<TResultType> Get<TResultType>(string url)
        {
            try
            {
                using var request = UnityWebRequest.Get(url);

                request.SetRequestHeader("Content-Type", ContentTypeJson);

                var operation = request.SendWebRequest();

                while (!operation.isDone)
                    await Task.Yield();

                if (request.result != UnityWebRequest.Result.Success)
                    Debug.LogError($"Failed: {request.error}");

                var result = JsonUtility.FromJson<TResultType>(request.downloadHandler.text);;

                return result;
            }
            catch (Exception ex)
            {
                Debug.LogError($"{nameof(Get)} failed: {ex.Message}");
                return default;
            }
        }
        
        /*
        public static async Task<T> Get<T>(string url)
        {
            var request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Content-Type", ContentTypeJson);
            
            var operation = request.SendWebRequest();
 
            while(!operation.isDone) 
                await Task.Yield();

            var jsonResponse = request.downloadHandler.text;
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log($"Failed: {request.error}");
            }
 
            try
            {
                var result = JsonUtility.FromJson<T>(jsonResponse);
                Debug.Log($"Success: {request.downloadHandler.text}");
                return result;
            }
            catch(Exception ex)
            {
                Debug.LogError($"Could not parse response {jsonResponse}.{ex}");
                return default;
            }
                 
        }*/
        
        
        
        
        // internal
        // public static IEnumerator Get(string url, Action<UnityWebRequest> done = null) =>
        //     Request(url, UnityWebRequest.kHttpVerbGET, null, done);

        // internal static IEnumerator Post(string url, object o, Action<UnityWebRequest> done = null) =>
        //     Request(url, UnityWebRequest.kHttpVerbPOST, JsonConvert.SerializeObject(o), done);

        internal static IEnumerator Request(string url, string method, string data = null, Action<UnityWebRequest> done = null)
        {
            UnityWebRequest request;

            switch(method)
            {
                case UnityWebRequest.kHttpVerbGET:
                    request = UnityWebRequest.Get(url);
                    yield return request.SendWebRequest();
                    done?.Invoke(request);
                    break;
                case UnityWebRequest.kHttpVerbPOST:
                    request = UnityWebRequest.Post(url, data);
                    request.method = UnityWebRequest.kHttpVerbPOST;
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(data));
                    request.SetRequestHeader("Content-Type", ContentTypeJson);
                    request.SetRequestHeader("Accept", ContentTypeJson);
                    yield return request.SendWebRequest();
                    done?.Invoke(request);
                    break;
            }
        }
    }
}