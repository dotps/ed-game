using System;
using CodeBase.CameraLogic;
using CodeBase.Hero;
using CodeBase.Logic;
using CodeBase.Services.Factory;
using CodeBase.Services.Progress;
using CodeBase.StaticData;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Infrastructure.StateMachine
{
    public class LoadLevelState : IPayloadState<string>
    {
        private const string EnemySpawnerTag = "SpawnPoint";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;
        private readonly IStaticDataService _staticDataService;
        private readonly IUIFactory _uiFactory;
        private LevelStaticData _levelStaticData;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain, IGameFactory gameFactory, IProgressService progressService, IStaticDataService staticDataService, IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() => _loadingCurtain.Hide();

        private void OnLoaded()
        {
            InitUI();
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _gameFactory.ProgressReaders)
                progressReader.LoadProgress(_progressService.Progress);
        }

        private void InitGameWorld()
        {
            GetLevelStaticData();
            InitSpawners();
            
            GameObject hero = InitHero();

            CameraFollow(hero);
            InitHUD(hero);
            
            InitDroppedLoot();
        }

        private void GetLevelStaticData()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            _levelStaticData = _staticDataService.GetLevelData(sceneName);
        }

        private void InitUI() => 
            _uiFactory.CreateUIContainer();

        private void InitDroppedLoot() => 
            _gameFactory.CreateDroppedLoot();

        private GameObject InitHero()
        {
            Vector3 initialPoint = _levelStaticData.initPlayerPosition;
            GameObject hero = _gameFactory.CreateHero(initialPoint);
            return hero;
        }

        private void InitSpawners()
        {
            foreach (var spawner in _levelStaticData.enemySpawners)
                _gameFactory.CreateSpawner(spawner.position, spawner.id, spawner.monsterTypeId);
        }

        private void InitHUD(GameObject hero)
        {
            GameObject hud = _gameFactory.CreateHUD();
            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
        }

        private static void CameraFollow(GameObject target) => 
            Camera.main.GetComponent<CameraFollow>().Follow(target);

    }
}