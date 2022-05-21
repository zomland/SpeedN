 using System.Collections;
using System.Collections.Generic;
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

        public void SignInWithGoogleClick()
        {
            FirebaseApi.Instance.SignInWithGoogle(OnSignInCallback);
        }

        public void SignInWithEmail()
        {
            FirebaseApi.Instance.SignInWithEmailAndPassword(emailSigninInput.text, passwordSigninInput.text, 
                OnSignInCallback);
        }
        
        public void SignUpWithEmail()
        {
            FirebaseApi.Instance.SignUpWithEmailAndPassword(emailSigninInput.text, passwordSigninInput.text,
                OnSignInCallback);
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

