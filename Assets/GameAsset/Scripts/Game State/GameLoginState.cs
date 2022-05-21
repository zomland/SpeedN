
using System.Threading;
using FirebaseHandler;
using Global;

namespace Runtime
{
    using System.Collections;
    using System.Collections.Generic;
    using Base.Pattern;
    using UnityEngine;
    using Cysharp.Threading.Tasks;
    using UnityEngine.SceneManagement;
    
    public class GameLoginState : GameState
    {
        [SerializeField] private bool debug;

        private CancellationTokenSource _cancellation;
        
        public override void EnterStateBehaviour(float dt, GameState fromState)
        {
            _cancellation = new CancellationTokenSource();
            var loginScene = SceneManager.GetSceneByName("LoginScene");
            if (!loginScene.isLoaded)
            {
                LoadSceneAsync("LoginScene").Forget();
            }
        }

        public override void UpdateBehaviour(float dt)
        {
            return;
        }

        public override void ExitStateBehaviour(float dt, GameState toState)
        {
            _cancellation.Dispose();
            GameStateParam.MainState = false;
            var loginScene = SceneManager.GetSceneByName("LoginScene");
            if (loginScene.isLoaded)
            {
                UnloadSceneAsync("LoginScene").Forget();
            }
        }

        public override void CheckExitTransition()
        {
            if (GameStateParam.MainState || debug)
            {
                _cancellation.Cancel();
                GameStateController.EnqueueTransition<GameMainState>();
            }
        }

        private async UniTask LoadSceneAsync(string sceneName)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            await asyncOperation;
            
            FirebaseApi.Instance.InitApi();
        }

        private async UniTask UnloadSceneAsync(string sceneName)
        {
            var asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
            await asyncOperation;
        }
    }
}

