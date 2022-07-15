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
        #region =============================================Auth=============================================
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
        #endregion=============================================Auth=============================================

        #region=============================================Database=============================================

        public void SetUpDatabaseRef()
        {
            _databaseHandler.SetUpReferences();
        }
        public async UniTaskVoid PostUser(DatabaseCallback callback)
        {
            _databaseHandler.PostUser(ClientData.Instance.ClientUser, callback).Forget();

            await UniTask.Yield();
        }

        public async UniTaskVoid PostUserValue(string valueKey, System.Object newValue, DatabaseCallback callback)
        {
            _databaseHandler.PostUserValue(ClientData.Instance.ClientUser, valueKey, newValue, callback)
                .Forget();

            await UniTask.Yield();
        }

        public async UniTaskVoid GetUserData(DatabaseCallback callback)
        {
            _databaseHandler.GetUserData(ClientData.Instance.ClientUser, callback).Forget();

            await UniTask.Yield();
        }

        public void RemoveUser(DatabaseCallback callback)
        {
            _databaseHandler.RemoveUser(callback);
        }

        public async UniTaskVoid AddAMovingRecord(MovingRecord _movingRecord, DatabaseCallback callback)
        {
            _databaseHandler.AddAMovingRecord(_movingRecord, ClientData.Instance.ClientUser.clientMovingRecord, callback).Forget();
            await UniTask.Yield();
        }
        public async UniTaskVoid PostClientVehicle(DatabaseCallback callback)
        {
            _databaseHandler.PostClientVehicle(callback).Forget();
            await UniTask.Yield();
        }
        public async UniTaskVoid GetModelVehicle(DatabaseCallback callback)
        {
            _databaseHandler.GetModelVehicle(callback).Forget();
            await UniTask.Yield();
        }
        public async UniTaskVoid PostClientStation(DatabaseCallback callback)
        {
            _databaseHandler.PostClientStation(callback).Forget();
            await UniTask.Yield();
        }
        public async UniTaskVoid GetServerStation(DatabaseCallback callback)
        {
            _databaseHandler.GetServerStation(callback).Forget();
            await UniTask.Yield();
        }
        #endregion=============================================Database=============================================
    }
}

