 using System.Collections;
using System.Collections.Generic;
 using Cysharp.Threading.Tasks;
 using Firebase.Auth;
using UnityEngine;
using FirebaseHandler;
using Global;
using UnityEngine.UI;

namespace Runtime.Controller
{
    public class LoginSceneController : MonoBehaviour
    {
        [SerializeField] private InputField emailSigninInput;
        [SerializeField] private InputField passwordSigninInput;

        public async void SignInWithGoogleClick()
        {
            GameManager.Instance.ShowLoading();
            FirebaseApi.Instance.SignInWithGoogle(OnSignInCallback).Forget();
            
            GameManager.Instance.HideLoading();
        }

        public async void SignInWithEmail()
        {
            GameManager.Instance.ShowLoading();
            FirebaseApi.Instance.SignInWithEmailAndPassword(emailSigninInput.text, passwordSigninInput.text, 
                OnSignInCallback).Forget();

            await UniTask.Yield();
            GameManager.Instance.HideLoading();
        }
        
        public async void SignUpWithEmail()
        {
            GameManager.Instance.ShowLoading();
            FirebaseApi.Instance.SignUpWithEmailAndPassword(emailSigninInput.text, passwordSigninInput.text,
                OnSignInCallback).Forget();

            await UniTask.Yield();
            
            GameManager.Instance.HideLoading();
        }

        private void OnSignInCallback(FirebaseUser user, string message, AuthError errorId)
        {
            if (errorId == AuthError.Failure)
            {
                
            }
            else if (errorId == AuthError.Cancelled)
            {
                
            }
            else if (errorId == AuthError.None)
            {
                GameStateParam.MainState = true;
            }
        }
    }
}

