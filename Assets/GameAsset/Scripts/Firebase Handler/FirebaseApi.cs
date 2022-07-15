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

        public async UniTask PostUser(DatabaseCallback callback)
        {
            await _databaseHandler.PostUser(ClientData.Instance.ClientUser, callback);
        }

        public async UniTask PostUserValue(string valueKey, System.Object newValue, DatabaseCallback callback)
        {
            await _databaseHandler.PostUserValue(ClientData.Instance.ClientUser, valueKey, newValue, callback);
        }

        public async UniTask GetUserData(DatabaseCallback callback)
        {
            await _databaseHandler.GetUserData(ClientData.Instance.ClientUser, callback);
        }

        public async UniTask RemoveUser(DatabaseCallback callback)
        {
            await _databaseHandler.RemoveUser(callback);
        }

        public async UniTask AddAMovingRecord(MovingRecord _movingRecord, DatabaseCallback callback)
        {
            await _databaseHandler.AddAMovingRecord(_movingRecord, ClientData.Instance.ClientUser.clientMovingRecord, callback);
        }

        public async UniTask PostClientVehicle(DatabaseCallback callback)
        {
            await _databaseHandler.PostClientVehicle(callback);
        }

        public async UniTask GetModelVehicle(DatabaseCallback callback)
        {
            await _databaseHandler.GetModelVehicle(callback);
        }

        public async UniTask PostClientStation(DatabaseCallback callback)
        {
            await _databaseHandler.PostClientStation(callback);
        }

        public async UniTask GetServerStation(DatabaseCallback callback)
        {
            await _databaseHandler.GetServerStation(callback);
        }

        public async UniTask PostServerStationOwner(string _stationID, string _ownerID, DatabaseCallback callback)
        {
            await _databaseHandler.PostServerStationOwner(_stationID, _ownerID, callback);
        }
        
        public async UniTask GetServerStation(string _stationID, float _priceEnergy, float _priceRepair, DatabaseCallback callback)
        {
            await _databaseHandler.PostServerStationPrice(_stationID, _priceEnergy, _priceRepair, callback);
        }
        #endregion=============================================Database=============================================
    }
}

