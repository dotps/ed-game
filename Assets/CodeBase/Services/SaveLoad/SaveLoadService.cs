using CodeBase.Data;
using CodeBase.Services.Factory;
using CodeBase.Services.Progress;
using UnityEngine;

namespace CodeBase.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        
        private readonly IStorageProgressService _playerProgress;
        private readonly IGameFactory _gameFactory;

        public SaveLoadService(IStorageProgressService progressService, IGameFactory gameFactory)
        {
            _playerProgress = progressService;
            _gameFactory = gameFactory;
        }
        
        public void SaveProgress()
        {
            foreach (var progressWriter in _gameFactory.ProgressWriters)
                progressWriter.UpdateProgress(_playerProgress.Progress);
            PlayerPrefs.SetString(ProgressKey, _playerProgress.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
    }
}