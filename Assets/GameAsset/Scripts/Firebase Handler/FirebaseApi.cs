using System;
using System.Collections;
using System.Collections.Generic;
using Base;
using Firebase.Auth;
using UnityEngine;
using System.Threading.Tasks;
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

        public void SignInWithGoogle(AuthCallback callback)
        {
            _authHandler.SignInWithGoogle(callback);
        }

        public void SignUpWithEmailAndPassword(string email, string password, AuthCallback callback)
        {
            //Debug.LogWarning(email);
            //Debug.LogWarning(password);
            _authHandler.SignUpWithEmail(email, password, callback);
        }

        public void SignInWithEmailAndPassword(string email, string password, AuthCallback callback)
        {
            _authHandler.SignInWithEmail(email, password, callback);
        }

        public void SignOut()
        {
            _authHandler.SignOut();
        }

        public void PostUser(ClientUser user)
        {
            _databaseHandler.PostUser(user, OnFirebaseDatabaseHandling);
        }

        public void PostUserValue(string valueKey, System.Object newValue)
        {
            _databaseHandler.PostUserValue(ClientData.Instance.ClientUser, valueKey, newValue, OnFirebaseDatabaseHandling);
        }

        public void AddNewUser(ClientUser user)
        {
            _databaseHandler.AddNewUser(user, OnFirebaseDatabaseHandling);
        }

        public void GetUserData()
        {
            _databaseHandler.GetUserData(ClientData.Instance.ClientUser, OnFirebaseDatabaseHandling);
        }

        public void CheckUserExisted()
        {
            _databaseHandler.CheckUserExisted(ClientData.Instance.ClientUser, OnFirebaseDatabaseHandling);
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

