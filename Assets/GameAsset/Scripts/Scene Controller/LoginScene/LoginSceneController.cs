using Firebase.Auth;
using FirebaseHandler;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;


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

        [Header("Loading Data")]
        public GameObject LoadingDataPage;
        public Slider LoadDataSlider;
        public Text textMess;

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

        #region ================== Check User SignUp Input =================
        public void CheckEmail_SignUpInput()
        {
            popup.SetActive(false);
            if (emailSignupInput.text == "")
            {
                textPopup.text = "Your email can not null";
                popup.SetActive(true);
            }
            if (isValidEmail(emailSignupInput.text) == false)
            {
                textPopup.text = "Incorrect email format";
                popup.SetActive(true);
            }
        }
        public void CheckPasword_SignUpInput()
        {
            popup.SetActive(false);
            if (passwordSignupInput.text == "")
            {
                textPopup.text = "Password can not null";
                popup.SetActive(true);
            }
            if (passwordSignupInput.text.Length < 7)
            {
                textPopup.text = "Password must contain at least 8 character";
                popup.SetActive(true);
            }
        }
        public void CheckPasswordComfirm_SignUpInput()
        {
            popup.SetActive(false);
            if (passwordSignupInput.text != passwordconfirmSignupInput.text)
            {
                textPopup.text = "Password confirmation does not match";
                popup.SetActive(true);
            }
        }
        #endregion

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
                            FirebaseApi.Instance.SignUpWithEmailAndPassword(emailSignupInput.text, passwordSignupInput.text,
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

            }
        }

        public void showLoadingDataPage(float _percentLoad)
        {
            textMess.text = "Loading..." + (_percentLoad * 100).ToString() + "%";
            LoadDataSlider.value = _percentLoad;
            Debug.Log("Loading..." + (_percentLoad * 100).ToString() + "%");
        }

        public void ActiveLoadingPage()
        {
            LoadingDataPage.SetActive(true);
        }
    }
}

