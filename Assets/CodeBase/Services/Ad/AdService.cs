using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace CodeBase.Services.Ad
{
    public class AdService : IAdService, IUnityAdsListener
    {
        private const string AndroidGameId = "5191509";
        private const string IOSGameId = "5191508";
        private const string RewardedVideoPlacementId = "Rewarded_Android"; // Unit Id
        private string _gameId;
        private Action _onVideoFinished;

        public event Action RewardedVideoReady;

        public int RewardValue => 10;

        public void Init()
        {
            _gameId = GetGameIdByPlatform();
            Advertisement.AddListener(this);
            Advertisement.Initialize(_gameId);
        }

        public void ShowRewardedVideo(Action onVideoFinished)
        {
            Advertisement.Show(RewardedVideoPlacementId);
            _onVideoFinished = onVideoFinished;
        }

        private string GetGameIdByPlatform()
        {
            string gameId = null;
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    gameId = AndroidGameId;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    gameId = IOSGameId;
                    break;
                case RuntimePlatform.WindowsEditor:
                    gameId = AndroidGameId;
                    break;
                default:
                    Debug.Log("Unsupported platform for Ads");
                    break;
            }
            return gameId;
        }

        public bool IsRewardedVideoReady =>
            Advertisement.IsReady(RewardedVideoPlacementId);

        public void OnUnityAdsReady(string placementId)
        {
            Debug.Log($"OnUnityAdsReady {placementId}");
            if (placementId == RewardedVideoPlacementId)
                RewardedVideoReady?.Invoke();
        }

        public void OnUnityAdsDidError(string message) => 
            Debug.Log($"OnUnityAdsDidError {message}");

        public void OnUnityAdsDidStart(string placementId) =>
            Debug.Log($"OnUnityAdsDidStart {placementId}");

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            switch (showResult)
            {
                case ShowResult.Failed:
                    Debug.Log($"OnUnityAdsDidFinish {showResult}");
                    break;
                case ShowResult.Skipped:
                    Debug.Log($"OnUnityAdsDidFinish {showResult}");
                    break;
                case ShowResult.Finished:
                    _onVideoFinished?.Invoke();
                    break;
                default:
                    Debug.Log($"OnUnityAdsDidFinish {showResult}");
                    break;
            }

            _onVideoFinished = null;
        }
    }
}