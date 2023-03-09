using System;
using CodeBase.Data;
using CodeBase.Services.Progress;
using CodeBase.Services.SaveLoad;

namespace CodeBase.Infrastructure.StateMachine
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IStorageProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private PlayerProgress _playerProgress;

        public LoadProgressState(GameStateMachine stateMachine, IStorageProgressService progressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInit();
            _stateMachine.Enter<LoadLevelState, string>(_progressService.Progress.worldData.positionOnLevel.levelName);
        }

        public void Exit()
        {
            
        }

        private void LoadProgressOrInit() => 
            _progressService.Progress = _saveLoadService.LoadProgress() ?? InitProgress();

        private PlayerProgress InitProgress()
        {
            _playerProgress = new PlayerProgress("Main");
            
            _playerProgress.heroState.maxHp = 50;
            _playerProgress.heroState.ResetHP();

            _playerProgress.heroStats.damage = 5f;
            _playerProgress.heroStats.damageRadius = 1f;
            
            return _playerProgress;
        }
    }
}