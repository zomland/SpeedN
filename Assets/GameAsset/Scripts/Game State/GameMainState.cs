using System;
using System.Collections;
using System.Collections.Generic;
using Base.Helper;
using Base.MessageSystem;
using UnityEngine;
using Base.Pattern;
using Cysharp.Threading.Tasks;
using Global;
using UnityEngine.SceneManagement;

namespace Runtime
{
    public class GameMainState : GameState
    {
        public override void EnterStateBehaviour(float dt, GameState fromState)
        {
            var scene = SceneManager.GetSceneByName("HomeScene");
            if (!scene.isLoaded)
            {
                LoadSceneAsync("HomeScene").Forget();
            }
            
            RegisterListener();
        }

        public override void UpdateBehaviour(float dt)
        {
            return;
        }

        public override void CheckExitTransition()
        {
            if (GameStateParam.LoginState)
            {
                GameStateController.EnqueueTransition<GameLoginState>();
                GameStateParam.LoginState = false;
            }
        }

        public override void ExitStateBehaviour(float dt, GameState toState)
        {
            var scene = SceneManager.GetSceneByName("HomeScene");
            if (scene.isLoaded)
            {
                UnloadSceneAsync("HomeScene").Forget();
            }

            var accountScene = SceneManager.GetSceneByName("AccountScene");
            if (accountScene.isLoaded)
            {
                UnloadSceneAsync("AccountScene").Forget();
            }
            
            RemoveListener();
        }

        private async UniTask LoadSceneAsync(string sceneName)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            await asyncOperation;
        }

        private async UniTask UnloadSceneAsync(string sceneName)
        {
            var asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
            await asyncOperation;
        }

        private void RegisterListener()
        {
            Messenger.RegisterListener<Scenes, Scenes>(Message.LoadScene, OnLoadSceneMessage);
        }

        private void RemoveListener()
        {
            Messenger.RemoveListener<Scenes, Scenes>(Message.LoadScene, OnLoadSceneMessage);
        }

        private void OnLoadSceneMessage(Scenes sceneToLoad, Scenes sceneToUnload)
        {
            if (sceneToLoad == sceneToUnload) return;

            Scene toScene = SceneManager.GetSceneByName(sceneToLoad.GetStringValue());
            Scene fromScene = SceneManager.GetSceneByName(sceneToUnload.GetStringValue());
            if (fromScene.isLoaded)
            {
                UnloadSceneAsync(fromScene.name).Forget();
            }

            if (!toScene.isLoaded)
            {
                LoadSceneAsync(sceneToLoad.GetStringValue()).Forget();
            }
        }
    }
}

