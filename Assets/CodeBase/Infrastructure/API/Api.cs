using System;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace CodeBase.Infrastructure.API
{
    /*
     * https://www.youtube.com/watch?v=Yp8uPxEn6Vg
     */
    
    public abstract class Api
    {
        private const string ContentTypeJson = "application/json";

        public async Task<string> GetRequest(string url, string token = null)
        {
            try
            {
                using var request = UnityWebRequest.Get(url);

                request.SetRequestHeader("Content-Type", ContentTypeJson);
                
                if (token != null)
                    request.SetRequestHeader("Authorization", "Bearer " + token);

                var operation = request.SendWebRequest();

                while (!operation.isDone)
                    await Task.Yield();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log($"Failed: {request.error}");
                    return null;
                }

                return request.downloadHandler.text;
            }
            catch(Exception error)
            {
                Debug.LogError($"{nameof(GetRequest)} failed: {error.Message}");
                return default;
            }
        }
        // public async Task<TResultType> GetRequest<TResultType>(string url, string token = null)
    }
}