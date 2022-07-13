using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Global;

namespace FirebaseHandler
{
    public class FirebaseDatabaseHandler
    {
        DatabaseReference databaseUsersRef;
        DatabaseReference databaseAssetsRef;
        DatabaseReference databaseMovingRecordsRef;
        DatabaseReference databaseClientUserRef;
        DatabaseReference databaseClientMovingRecordRef;

        ~FirebaseDatabaseHandler()
        {

        }

        public void InitializeDatabase()
        {
            SetUpGeneralReferences();
            SetUpGeneralEvents();
        }

        public void InitialSetUpClient(ClientUser user
            , DatabaseCallback callbackUser, DatabaseCallback callbackMovingRecord)
        {
            InitialSetUpUser(user, callbackUser).Forget();
            InitialSetUpMovingRecord(user, callbackMovingRecord).Forget();
        }

        void SetUpGeneralReferences()
        {
            databaseUsersRef = FirebaseDatabase.DefaultInstance.GetReference("Users");
            databaseMovingRecordsRef = FirebaseDatabase.DefaultInstance.GetReference("MovingRecords");
            databaseAssetsRef = FirebaseDatabase.DefaultInstance.GetReference("Assets");
        }
        void SetUpGeneralEvents()
        {
            //Event users
            databaseUsersRef.ChildAdded += HandleUserAdded;
            databaseUsersRef.ChildRemoved += HandleUserRemoved;
            //Event asset
            databaseAssetsRef.ValueChanged += HandleAssetsValueChanged;
        }

        void SetUpUserRef(string _useKey)
        {
            databaseClientUserRef = databaseUsersRef.Child(_useKey);
            databaseClientUserRef.ValueChanged += HandleUserValueChanged;
        }
        void SetUpMovingRecordRef(string _useKey)
        {
            databaseClientMovingRecordRef = databaseMovingRecordsRef.Child(_useKey);
            databaseClientMovingRecordRef.ChildAdded += HandleMovingRecordAdded;
        }

        #region User method
        public async UniTaskVoid InitialSetUpUser(ClientUser user, DatabaseCallback databaseCallback)
        {
            await databaseUsersRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                bool isUserExisted = false;
                if (task.IsFaulted)
                {
                    //DisplayError("connect database failed");
                    databaseCallback.Invoke("InitialSetUpUser", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    DataSnapshot clientSnapshot = snapshot.Child(user.userKey);
                    if (clientSnapshot.GetValue(true) == null)
                    {
                        isUserExisted = false;
                    }
                    else
                    {
                        isUserExisted = true;
                        string JsonData = JsonConvert.SerializeObject(clientSnapshot.GetValue(true));
                        JsonUtility.FromJsonOverwrite(JsonData, user);
                        SetUpUserRef(user.userKey);
                        databaseCallback.Invoke("InitialSetUpUser", "user existed: Get data user", 0);

                    }
                }
                if (!isUserExisted)
                {
                    string JsonData = user.GetStringJsonData();
                    databaseUsersRef.Child(user.userKey).SetRawJsonValueAsync(JsonData);
                    SetUpUserRef(user.userKey);
                    databaseCallback.Invoke("InitialSetUpUser", "new user: Add success", 0);
                }
            });
        }
        public async UniTaskVoid PostUser(ClientUser user, DatabaseCallback databaseCallback)
        {
            await databaseClientUserRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                bool isUserExisted = false;
                if (task.IsFaulted)
                {
                    //DisplayError("connect database failed");
                    databaseCallback.Invoke("PostUser", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    if (snapshot.GetValue(true) == null)
                    {
                        isUserExisted = false;
                    }
                    else
                    {
                        isUserExisted = true;
                    }
                }
                if (isUserExisted)
                {
                    string JsonData = JsonConvert.SerializeObject(user);
                    databaseClientUserRef.SetRawJsonValueAsync(JsonData);
                    //DisplayWarning("PostUser: post success");
                    databaseCallback.Invoke("PostUser", "success", 0);
                }
                else
                {
                    //DisplayWarning("Post User: User not existed");
                    databaseCallback.Invoke("PostUser", "User not existed", 0);
                }
            });
        }
        public async UniTaskVoid PostUserValue(ClientUser user, string valueKey, System.Object newValue, DatabaseCallback databaseCallback)
        {
            await databaseClientUserRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                bool isUserExisted = false;
                if (task.IsFaulted)
                {
                    //DisplayError("connect database failed");
                    databaseCallback.Invoke("PostUserValue", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    if (snapshot.GetValue(true) == null)
                    {
                        isUserExisted = false;
                    }
                    else
                    {
                        isUserExisted = true;
                    }
                }
                if (isUserExisted)
                {
                    string json = JsonConvert.SerializeObject(newValue);
                    databaseClientUserRef.Child(valueKey).SetRawJsonValueAsync(json);
                    databaseCallback.Invoke("PostUserValue", "success", 0);
                }
                else
                {
                    databaseCallback.Invoke("PostUserValue", "User not existed", 0);
                }
            });
        }
        public async UniTaskVoid PostUserNFT(ClientUser user, TypeNFT typeNFT, System.Object newValue
            , DatabaseCallback databaseCallback)
        {
            string NFTKey = "";
            switch (typeNFT)
            {
                case TypeNFT.Gem:
                    NFTKey = "clientGems";
                    break;
                case TypeNFT.Blueprint:
                    NFTKey = "clientBlueprints";
                    break;
                case TypeNFT.Vehicle:
                    NFTKey = "vehicleControllers";
                    break;
            }
            await databaseClientUserRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                bool isUserExisted = false;
                if (task.IsFaulted)
                {
                    databaseCallback.Invoke("PostUserNFT", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    if (snapshot.GetValue(true) == null)
                    {
                        isUserExisted = false;
                    }
                    else
                    {
                        isUserExisted = true;
                    }
                }
                if (isUserExisted)
                {
                    string json = JsonConvert.SerializeObject(newValue);
                    databaseClientUserRef.Child("clientNFT").Child(NFTKey).SetRawJsonValueAsync(json);
                    databaseCallback.Invoke("PostUserNFT: " + NFTKey, "success", 0);
                }
                else
                {
                    databaseCallback.Invoke("PostUserNFT: " + NFTKey, "User not existed", 0);
                }
            });
        }

        public async UniTaskVoid GetUserData(ClientUser user, DatabaseCallback databaseCallback)
        {
            await databaseClientUserRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    //DisplayError("Get data user failed");
                    databaseCallback.Invoke("GetUser", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    string JsonData = JsonConvert.SerializeObject(snapshot.GetValue(true));
                    JsonUtility.FromJsonOverwrite(JsonData, user);
                    Debug.Log(JsonData);
                    //DisplayMessage("Get data success");
                    databaseCallback.Invoke("GetUser", "Get data success", 0);
                }
            });
        }
        public void RemoveUser(DatabaseCallback databaseCallback)
        {
            //Delete Account
            databaseClientUserRef.SetValueAsync(null);
            databaseClientMovingRecordRef.SetValueAsync(null);
            databaseCallback.Invoke("RemoveUser", "User is removed", 0);
        }

        public async UniTaskVoid CheckUserExisted(ClientUser user, DatabaseCallback databaseCallback)
        {
            await databaseClientUserRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                bool isUserExisted = false;
                if (task.IsFaulted)
                {
                    //DisplayError("Check data user failed: cant connect database");
                    databaseCallback.Invoke("CheckUserExisted", "connect database failed", 0);
                    isUserExisted = false;
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...

                    if (snapshot.GetValue(true) == null)
                    {
                        isUserExisted = false;
                    }
                    else
                    {
                        isUserExisted = true;
                    }
                }
                if (isUserExisted)
                {
                    databaseCallback.Invoke("CheckUserExisted", "User existed", 0);
                }
                else
                {
                    databaseCallback.Invoke("CheckUserExisted", "username does not exist -> new user or error", 0);
                }
            });
        }
        #endregion User method

        #region MovingRecord method
        public async UniTaskVoid InitialSetUpMovingRecord(ClientUser user
            , DatabaseCallback databaseCallback)
        {
            await databaseMovingRecordsRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                bool isExisted = false;
                if (task.IsFaulted)
                {
                    //DisplayError("connect database failed");
                    databaseCallback.Invoke("InitialSetUpMovingRecord", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    DataSnapshot clientSnapshot = snapshot.Child(user.userKey);
                    if (clientSnapshot.GetValue(true) == null)
                    {
                        isExisted = false;
                    }
                    else
                    {
                        isExisted = true;
                        string JsonData = JsonConvert.SerializeObject(clientSnapshot.GetValue(true));
                        ClientData.Instance.ClientUser.clientMovingRecord = JsonConvert.DeserializeObject<ClientMovingRecord>(JsonData);
                        SetUpMovingRecordRef(user.userKey);
                        ClientData.Instance.ClientUser.clientMovingRecord.DeleteExpiredRecord();
                        JsonData = JsonConvert.SerializeObject
                            (ClientData.Instance.ClientUser.clientMovingRecord.movingRecords);
                        databaseClientMovingRecordRef.Child("movingRecordDetails")
                            .SetRawJsonValueAsync(JsonData);
                        databaseCallback.Invoke("InitialSetUpMovingRecord", "user existed : get data", 0);
                    }
                }
                if (!isExisted)
                {
                    ClientData.Instance.ClientUser.clientMovingRecord.AddMovingRecordDetail(new MovingRecord());
                    databaseMovingRecordsRef.Child(user.userKey)
                        .SetRawJsonValueAsync(ClientData.Instance.ClientUser.clientMovingRecord.GetStringJsonData());
                    SetUpMovingRecordRef(user.userKey);
                    databaseCallback.Invoke("InitialSetUpMovingRecord", "new user : Add data", 0);
                }
            });
        }

        public async UniTaskVoid AddAMovingRecord(float _totalTime, float _totalKm, MovingRecord _movingRecord
            , ClientMovingRecord clientMovingRecord, DatabaseCallback databaseCallback)
        {
            await databaseClientMovingRecordRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    //DisplayError("connect database failed");
                    databaseCallback.Invoke("AddMovingRecord", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    // Do something with snapshot...
                    string json = _movingRecord.GetStringJsonData();
                    databaseClientMovingRecordRef.Child("totalTime").SetValueAsync(_totalTime);
                    databaseClientMovingRecordRef.Child("totalKm").SetValueAsync(_totalKm);
                    databaseClientMovingRecordRef.Child("movingRecordDetails")
                        .Child(_movingRecord.RecordID).SetRawJsonValueAsync(json);
                    databaseCallback.Invoke("AddMovingRecord", "Add success", 0);
                }
            });
        }

        public async UniTaskVoid GetMovingRecordsData(ClientUser user
            , ClientMovingRecord clientMovingRecord, DatabaseCallback databaseCallback)
        {
            await databaseClientMovingRecordRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    //DisplayError("Get data user failed");
                    databaseCallback.Invoke("GetMovingRecordsData", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    string JsonData = JsonConvert.SerializeObject(snapshot.GetValue(true));
                    JsonUtility.FromJsonOverwrite(JsonData, clientMovingRecord);
                    Debug.Log(JsonData);
                    //DisplayMessage("Get data success");
                    databaseCallback.Invoke("GetMovingRecordsData", "Get data success", 0);
                }
            });
        }

        #endregion movingRecord method

        #region Handler Events
        void HandleUserValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                DisplayError(args.DatabaseError.Message);
                return;
            }
            DisplayMessage("Data of client user have changes");
        }

        void HandleAssetsValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                DisplayError(args.DatabaseError.Message);
                return;
            }
            DisplayMessage("Data of assets have changes");
        }

        void HandleUserAdded(object sender, ChildChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                DisplayError(args.DatabaseError.Message);
                return;
            }
            // Do something with the data in args.Snapshot
            DisplayMessage("User Added");

        }

        void HandleUserRemoved(object sender, ChildChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                Debug.LogError(args.DatabaseError.Message);
                return;
            }
            // Do something with the data in args.Snapshot
            DisplayMessage("User Remove");
        }

        void HandleMovingRecordAdded(object sender, ChildChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                DisplayError(args.DatabaseError.Message);
                return;
            }
            // Do something with the data in args.Snapshot
            DisplayMessage("Moving Record Added");
        }

        void HandleChildMoved(object sender, ChildChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                Debug.LogError(args.DatabaseError.Message);
                return;
            }
            // Do something with the data in args.Snapshot
            DisplayMessage("User.ChildMoved");
        }
        #endregion Handler Events

        #region DisplayMessage
        void DisplayMessage(string message = "")
        {
            Debug.Log("Firebase-database-notify: " + message);
        }

        void DisplayWarning(string warning)
        {
            Debug.LogWarning("Firebase-database-warning: " + warning);
        }

        void DisplayError(string error)
        {
            Debug.LogError("Firebase-database-error: " + error);
        }
        #endregion DisplayMessage
    }
}
