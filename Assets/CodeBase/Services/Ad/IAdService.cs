using System;

namespace CodeBase.Services.Ad
{
    public interface IAdService : IService
    {
        event Action RewardedVideoReady;
        bool IsRewardedVideoReady { get; }
        int RewardValue { get; }
        void Init();
        void ShowRewardedVideo(Action onVideoFinished);
    }
}