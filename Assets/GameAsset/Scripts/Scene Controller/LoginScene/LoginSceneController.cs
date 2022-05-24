using System.Collections;
using System.Collections.Generic;
using Firebase.Auth;
using UnityEngine;
using FirebaseHandler;
using Global;
using UnityEngine.UI;
using System.Text.RegularExpressions;


namespace Runtime.Controller
{
    public class LoginSceneController : MonoBehaviour
    {
        [SerializeField] private InputField emailSigninInput;
        [SerializeField] private InputField passwordSigninInput;

        [SerializeField] private InputField emailSignupInput;
        [SerializeField] private InputField passwordSignupInput;
        [SerializeField] private InputField passwordconfirmSignupInput;

        [SerializeField] private GameObject SignIn;
        [SerializeField] private GameObject SignUp;
        [SerializeField] private GameObject popup;
        [SerializeField] private Text textPopup;
        void Start()
        {
            popup.SetActive(false);
        }
        public void SignInWithGoogleClick()
        {
            FirebaseApi.Instance.SignInWithGoogle(OnSignInCallback).Forget();
        }

        public void SignInWithEmail()
        {
            FirebaseApi.Instance.SignInWithEmailAndPassword(emailSigninInput.text, passwordSigninInput.text,
                OnSignInCallback).Forget();
        }
        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z]+[a-zA-Z0-9]+)@([\w\-]+)((\.(com))+)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
        public void SignUpWithEmail()
        {
            if (emailSignupInput.text == "" || passwordSignupInput.text == "")
            {
                textPopup.text = "Password or username can not null";
                popup.SetActive(true);
            }
            else
            {
                if (isValidEmail(emailSignupInput.text) == true)
                {
                    if (passwordSignupInput.text.Length < 7)
                    {
                        popup.SetActive(true);
                        textPopup.text = "Password not enough characters";

                    }
                    else
                    {
                        if (passwordSignupInput.text == passwordconfirmSignupInput.text)
                        {
                            popup.SetActive(false);
                            SignIn.SetActive(true);
                            SignUp.SetActive(false);
                            FirebaseApi.Instance.SignUpWithEmailAndPassword(emailSigninInput.text, passwordSigninInput.text,
                                OnSignInCallback).Forget();
                        }
                        else
                        {
                            popup.SetActive(true);
                            textPopup.text = "Password is not the same";
                        }
                    }
                }
                else
                {
                    textPopup.text = "Username incorrect email format";
                    popup.SetActive(true);
                }
            }
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

