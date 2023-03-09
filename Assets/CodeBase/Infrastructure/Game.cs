using CodeBase.Infrastructure.StateMachine;
using CodeBase.Logic;
using CodeBase.Services;
using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, ServiceLocator.Instance);
        }
        
    }
}