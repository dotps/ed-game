using System;
using System.Collections.Generic;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.Factory;
using CodeBase.Services.Progress;
using CodeBase.Services.SaveLoad;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, ServiceLocator serviceLocator)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, serviceLocator),
                [typeof(LoadLevelState)] = new LoadLevelState(
                    this, 
                    sceneLoader,
                    loadingCurtain, 
                    serviceLocator.GetSingleInstance<IGameFactory>(), 
                    serviceLocator.GetSingleInstance<IProgressService>(), 
                    serviceLocator.GetSingleInstance<IStaticDataService>(),
                    serviceLocator.GetSingleInstance<IUIFactory>()
                ),
                [typeof(LoadProgressState)] = new LoadProgressState(
                    this, 
                    serviceLocator.GetSingleInstance<IProgressService>(), 
                    serviceLocator.GetSingleInstance<ISaveLoadService>()
                ),
                [typeof(GameLoopState)] = new GameLoopState(this) 
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }
        
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}