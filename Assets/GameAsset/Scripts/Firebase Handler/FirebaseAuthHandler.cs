using System.Collections;
using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Extensions;
using Global;
using Google;
using Newtonsoft.Json;
using UnityEngine;

namespace FirebaseHandler
{
    public class FirebaseAuthHandler
    {
        private FirebaseAuth _auth;
        private const string WebClientId = "182284506957-3b0v5e8a6cu0dgbortu3r7p6jr4r1p9b.apps.googleusercontent.com";
        private string _activeCredential = String.Empty;
        private bool _isAutoCheck = false;
        private Credential _prevCredential;

        ~FirebaseAuthHandler()
        {
            _auth.StateChanged -= OnAuthStateChanged;
            _auth.Dispose();
        }

        public void InitializeAuth()
        {
            _auth = FirebaseAuth.DefaultInstance;
            _auth.StateChanged += OnAuthStateChanged;
            _isAutoCheck = true;
            OnAuthStateChanged(this, null);
        }

        public void SignInWithGoogle(AuthCallback success)
        {
            if (GoogleSignIn.Configuration == null)
            {
                GoogleSignInConfiguration configuration = new GoogleSignInConfiguration()
                {
                    WebClientId = WebClientId, RequestIdToken = true
                };
                GoogleSignIn.Configuration = configuration;
                GoogleSignIn.Configuration.UseGameSignIn = false;
                GoogleSignIn.Configuration.RequestIdToken = true;
            }

            _isAutoCheck = false;

            Task<GoogleSignInUser> signIn = GoogleSignIn.DefaultInstance.SignIn();
            TaskCompletionSource<FirebaseUser> signInCompleted = new TaskCompletionSource<FirebaseUser>();

            signIn.ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Sign In Failed " + task.Exception.Message);
                    return;
                }
                if (task.IsCanceled)
                {
                    signInCompleted.SetCanceled();
                    return;
                }
                
                {
                    GoogleSignInUser googleUser = task.Result;
                    
                    Debug.Log("Result: " + JsonConvert.SerializeObject(googleUser));
                    
                    Credential credential = GoogleAuthProvider.GetCredential(googleUser.IdToken, null);
                    _auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWith(authTask =>
                    {
                        if (authTask.IsCanceled)
                        {
                            signInCompleted.SetCanceled();
                            success.Invoke(null, "Sign in was canceled", AuthError.Cancelled);
                        }
                        else if (authTask.IsFaulted)
                        {
                            Debug.Log("Sign In Failed");
                            signInCompleted.SetException(authTask.Exception);
                            success.Invoke(null, authTask.Exception.Message, AuthError.Failure);
                        }
                        else
                        {
                            Debug.Log("Result: " + JsonConvert.SerializeObject(authTask.Result));
                            signInCompleted.SetResult(authTask.Result.User);
                            success.Invoke(authTask.Result.User, "Success");
                        }
                    });
                }
            });
        }

        public void SignInWithEmail(string email, string password, AuthCallback callback)
        {
            _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Sign In Failed: " + task.Exception.Message);
                    callback.Invoke(null, task.Exception.Message, AuthError.Failure);
                    return;
                }
                
                if (task.IsCanceled)
                {
                    Debug.Log("Sign In Canceled");
                    callback.Invoke(null, "Signin process was canceled!", AuthError.Cancelled);
                    return;
                }
                
                Debug.Log("Sign In Success");
                callback.Invoke(task.Result, "Sign In Success");
            });
        }

        public void SignUpWithEmail(string email, string password, AuthCallback callback)
        {
            Debug.LogWarning(email+"handle");
            Debug.LogWarning(password + "handle");
            _auth.CreateUserWithEmailAndPasswordAsync(email, password)
                .ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled)
                    {
                        Debug.LogError("SignUpWithEmailAndPasswordAsync was canceled.");
                        callback.Invoke(null, "Signup process was canceled!", AuthError.Cancelled);
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        Debug.LogError("SignupWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                        callback.Invoke(null, task.Exception.Message, AuthError.Failure);
                        return;
                    }

                    FirebaseUser newUser = task.Result;
                    Debug.LogFormat("User signed in successfully");
                    callback.Invoke(task.Result, "Sign Up Success");
                });
        }

        public void LinkWithCredential(string credential)
        {
            
        }

        public void SignOut()
        {
            _auth.SignOut();
        }

        private void OnAuthStateChanged(object sender, System.EventArgs eventArgs)
        {
            if (sender is FirebaseAuth senderAuth)
            {
                if (senderAuth.CurrentUser != null && senderAuth.CurrentUser == _auth.CurrentUser)
                {
                    ClientData.Instance.clientUser.userID = senderAuth.CurrentUser.UserId;
                    ClientData.Instance.clientUser.userName = senderAuth.CurrentUser.DisplayName;
                    ClientData.Instance.clientUser.email = senderAuth.CurrentUser.Email;
                    ClientData.Instance.clientUser.CreateUserKey();
                    FirebaseApi.Instance.AddNewUser();
                    if (!_isAutoCheck) return;
                    GameStateParam.MainState = true;
                }
            }
        }
    }
}