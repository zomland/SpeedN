using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using Firebase.Auth;
using UnityEngine;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Global;
using Zenject;

namespace FirebaseHandler
{
    public class FirebaseApi : Singleton<FirebaseApi>
    {
        private FirebaseAuthHandler _authHandler;

        private FirebaseDatabaseHandler _databaseHandler;


        [Inject]
        public void Construct(FirebaseAuthHandler authHandler, FirebaseDatabaseHandler databaseHandler)
        {
            _authHandler = authHandler;
            _databaseHandler = databaseHandler;
        }

        public void InitApi()
        {
            _authHandler.InitializeAuth();
            _databaseHandler.InitializeDatabase();
        }

        public async UniTaskVoid SignInWithGoogle(AuthCallback callback)
        {
            _authHandler.SignInWithGoogle(callback).Forget();

            await UniTask.Yield();
        }

        public async UniTaskVoid SignUpWithEmailAndPassword(string email, string password, AuthCallback callback)
        {
            //Debug.LogWarning(email);
            //Debug.LogWarning(password);
            _authHandler.SignUpWithEmail(email, password, callback).Forget();

            await UniTask.Yield();
        }

        public async UniTaskVoid SignInWithEmailAndPassword(string email, string password, AuthCallback callback)
        {
           _authHandler.SignInWithEmail(email, password, callback).Forget();

           await UniTask.Yield();
        }

        public void SignOut()
        {
            _authHandler.SignOut();
        }

        public async UniTaskVoid PostUser(ClientUser user)
        {
            _databaseHandler.PostUser(user, OnFirebaseDatabaseHandling).Forget();

            await UniTask.Yield();
        }

        public async UniTaskVoid PostUserValue(string valueKey, System.Object newValue)
        {
            _databaseHandler.PostUserValue(ClientData.Instance.ClientUser, valueKey, newValue, OnFirebaseDatabaseHandling)
                .Forget();
            
            await UniTask.Yield();
        }

        public async UniTaskVoid AddNewUser(ClientUser user)
        {
            _databaseHandler.AddNewUser(user, OnFirebaseDatabaseHandling).Forget();
            await UniTask.Yield();
        }

        public async UniTaskVoid GetUserData()
        {
            _databaseHandler.GetUserData(ClientData.Instance.ClientUser, OnFirebaseDatabaseHandling).Forget();
            
            await UniTask.Yield();
        }

        public async UniTaskVoid CheckUserExisted()
        {
            _databaseHandler.CheckUserExisted(ClientData.Instance.ClientUser, OnFirebaseDatabaseHandling).Forget();
            
            await UniTask.Yield();
        }

        public void RemoveUser()
        {
            _databaseHandler.RemoveUser(ClientData.Instance.ClientUser, OnFirebaseDatabaseHandling);
        }

        void OnFirebaseDatabaseHandling(string nameProcedure, string message, int errorID)
        {
            string detailError = "";
            switch (errorID)
            {
                case (int)DatabaseErrorID.None:
                    detailError = "None error";
                    break;
                default:
                    break;
            }
            Debug.LogWarning(nameProcedure + ": " + message + ": " + detailError);
        }
    }
}

