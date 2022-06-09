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

        public async UniTaskVoid PostUser(ClientUser user, DatabaseCallback callback)
        {
            _databaseHandler.PostUser(user, callback).Forget();

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

        public async UniTaskVoid CheckUserExisted(DatabaseCallback callback)
        {
            _databaseHandler.CheckUserExisted(ClientData.Instance.ClientUser, callback).Forget();

            await UniTask.Yield();
        }

        public void RemoveUser(DatabaseCallback callback)
        {
            _databaseHandler.RemoveUser(callback);
        }

        public async UniTaskVoid AddAMovingRecord(MovingRecordDetail _movingRecord, DatabaseCallback callback)
        {
            float _totalKm = ClientData.Instance.clientMovingRecord.totalKm;
            float _totalTime = ClientData.Instance.clientMovingRecord.totalTime;
            _databaseHandler.AddAMovingRecord(_totalTime, _totalKm, _movingRecord, ClientData.Instance.clientMovingRecord
                , callback).Forget();
            await UniTask.Yield();
        }

        public async UniTaskVoid GetMovingRecords(DatabaseCallback callback)
        {
            _databaseHandler.GetMovingRecordsData(ClientData.Instance.ClientUser
                , ClientData.Instance.clientMovingRecord, callback).Forget();
            await UniTask.Yield();
        }

        public async UniTaskVoid InitialSetUpClient(DatabaseCallback callbackUser, DatabaseCallback callbackMovingRecord)
        {
            _databaseHandler.InitialSetUpClient(ClientData.Instance.ClientUser, callbackUser, callbackMovingRecord);
            await UniTask.Yield();
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

