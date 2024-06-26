﻿using System.Collections;
using UnityEngine.Networking;

namespace Networking.Requests
{
    public class BestPlayersRequest
    {
        public IEnumerator GetCoroutine(System.Action<string> callback)
        {
            var url = "http://localhost:5139/api/Player/GetBestPlayers";
            
            using var request = UnityWebRequest.Get(url);
            
            yield return request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                callback?.Invoke(string.Empty);
                yield break;
            }
            
            var json = request.downloadHandler.text;
            
            callback?.Invoke(json);
        }
    }
}