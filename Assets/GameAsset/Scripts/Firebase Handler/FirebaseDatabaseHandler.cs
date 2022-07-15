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
        DatabaseReference databaseRef;
        DatabaseReference databaseStationRef;
        DatabaseReference databaseModelVehicleRef;
        DatabaseReference databaseClientUserRef;
        DatabaseReference databaseClientVehicleRef;
        DatabaseReference databaseClientStationRef;
        DatabaseReference databaseClientMovingRecordRef;

        ~FirebaseDatabaseHandler()
        {

        }

        public void InitializeDatabase()
        {
            databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
        }

        public void SetUpReferences()
        {
            DatabaseReference databaseUsersRef = databaseRef.Child("Users");
            databaseStationRef = databaseRef.Child("Stations");
            databaseModelVehicleRef = databaseRef.Child("ModelVehicles");
            databaseClientUserRef = databaseUsersRef.Child(ClientData.Instance.ClientUser.userKey);
            databaseClientVehicleRef = databaseClientUserRef.Child("clientVehicle");
            databaseClientMovingRecordRef = databaseClientUserRef.Child("clientMovingRecord");
            //Set up event
            databaseStationRef.ValueChanged+=HandleServerStationChanged;
            databaseModelVehicleRef.ValueChanged+=HandleModelVehicleChanged;
            databaseClientUserRef.ValueChanged+=HandleUserChanged;
            databaseClientVehicleRef.ValueChanged+=HandleVehicleChanged;
            databaseClientMovingRecordRef.ValueChanged+=HandleMovingRecordChanged;
        }

        #region ===========================================User===========================================
        public async UniTask PostUser(ClientUser user, DatabaseCallback databaseCallback)
        {
            await databaseClientUserRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    databaseCallback.Invoke("PostUser", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    bool isUserExisted = snapshot.GetValue(true) != null;
                    if (isUserExisted)
                    {
                        string JsonData = JsonConvert.SerializeObject(user);
                        databaseClientUserRef.SetRawJsonValueAsync(JsonData);
                        databaseCallback.Invoke("PostUser", "success", 0);
                    }
                    else
                    {
                        databaseCallback.Invoke("PostUser", "User not existed", 0);
                    }
                }
            });
        }

        public async UniTask PostUserValue(ClientUser user, string valueKey, System.Object newValue, DatabaseCallback databaseCallback)
        {
            await databaseClientUserRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {

                if (task.IsFaulted)
                {
                    //DisplayError("connect database failed");
                    databaseCallback.Invoke("PostUserValue", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    bool isUserExisted = snapshot.GetValue(true) != null;
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
                }
            });
        }

        public async UniTask GetUserData(ClientUser user, DatabaseCallback databaseCallback)
        {
            await databaseClientUserRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    databaseCallback.Invoke("GetUser", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    bool isUserExisted = snapshot.GetValue(true) != null;
                    if (isUserExisted)
                    {
                        string JsonData = JsonConvert.SerializeObject(snapshot.GetValue(true));
                        user = JsonConvert.DeserializeObject<ClientUser>(JsonData);
                        Debug.Log(JsonData);
                        //DisplayMessage("Get data success");
                        databaseCallback.Invoke("GetUser", "Get data success", 0);
                    }
                    else
                    {
                        string JsonData = JsonConvert.SerializeObject(user);
                        databaseClientUserRef.SetRawJsonValueAsync(JsonData);
                        databaseCallback.Invoke("GetUser", "New User -> create data from local", 0);
                    }
                }
            });
        }
        public async UniTask RemoveUser(DatabaseCallback databaseCallback)
        {
            //Delete Account
            await databaseClientUserRef.SetValueAsync(null);
            databaseCallback.Invoke("RemoveUser", "User is removed", 0);
        }

        #endregion ===========================================User===========================================

        #region ===========================================Vehicle===========================================
        public async UniTask PostClientVehicle(DatabaseCallback databaseCallback)
        {
            await databaseClientVehicleRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {

                if (task.IsFaulted)
                {
                    databaseCallback.Invoke("PostUserNFT", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    bool isUserExisted = snapshot.GetValue(true) != null;
                    if (isUserExisted)
                    {
                        string json = JsonConvert.SerializeObject(ClientData.Instance.ClientUser.clientVehicle);
                        databaseClientVehicleRef.SetRawJsonValueAsync(json);
                        databaseCallback.Invoke("PostClientVehicle: ", "success", 0);
                    }
                    else
                    {
                        databaseCallback.Invoke("PostClientVehicle: ", "User not existed", 0);
                    }
                }
            });
        }
        public async UniTask GetModelVehicle(DatabaseCallback databaseCallback)
        {
            await databaseModelVehicleRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    databaseCallback.Invoke("GetModelVehicle", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    string JsonData = JsonConvert.SerializeObject(snapshot.GetValue(true));
                    ModelVehicle.ModelsDict = JsonConvert
                        .DeserializeObject<Dictionary<string, ModelVehicleBaseStats>>(JsonData);
                    Debug.Log(JsonConvert.SerializeObject(ModelVehicle.ModelsDict));
                    databaseCallback.Invoke("GetModelVehicle", "Get data success", 0);
                }
            });
        }
        #endregion ===========================================Vehicle==========================================

        #region ===========================================Moving Record===========================================
        public async UniTask AddAMovingRecord(MovingRecord _movingRecord
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
                    DataSnapshot snapshot = task.Result;

                    bool isUserExisted = snapshot.GetValue(true) != null;
                    if (isUserExisted)
                    {
                        string json = _movingRecord.GetStringJsonData();
                        databaseClientMovingRecordRef.Child(_movingRecord.RecordID).SetRawJsonValueAsync(json);
                        databaseCallback.Invoke("AddMovingRecord", "Add success", 0);
                    }
                    else
                    {
                        databaseCallback.Invoke("AddAMovingRecord: ", "User not existed", 0);
                    }
                }
            });
        }
        #endregion ===========================================Moving Record===========================================

        #region ===========================================Station===========================================
        public async UniTask PostClientStation(DatabaseCallback databaseCallback)
        {
            await databaseClientStationRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    databaseCallback.Invoke("PostClientStation", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    bool isUserExisted = snapshot.GetValue(true) != null;
                    if (isUserExisted)
                    {
                        string json = JsonConvert.SerializeObject(ClientData.Instance.ClientUser.clientStation);
                        databaseClientStationRef.SetRawJsonValueAsync(json);
                        databaseCallback.Invoke("PostClientStation: ", "success", 0);
                    }
                    else
                    {
                        databaseCallback.Invoke("PostClientStation: ", "User not existed", 0);
                    }
                }
            });
        }

        public async UniTask PostServerStationOwner(string _stationID, string _ownerID, DatabaseCallback databaseCallback)
        {
            await databaseStationRef.Child(_stationID).Child("ownerID").SetValueAsync(_ownerID);
            databaseCallback.Invoke("PostServerStationOwner: ", "success", 0);
        }

        public async UniTask PostServerStationPrice(string _stationID, float _priceEnergy, float _priceRepair, DatabaseCallback databaseCallback)
        {
            await databaseStationRef.Child(_stationID).Child("priceEnergy").SetValueAsync(_priceEnergy);
            await databaseStationRef.Child(_stationID).Child("priceRepair").SetValueAsync(_priceRepair);
            databaseCallback.Invoke("PostServerStationOwner: ", "success", 0);
        }

        public async UniTask GetServerStation(DatabaseCallback databaseCallback)
        {
            await databaseStationRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    databaseCallback.Invoke("GetServerStation", "connect database failed", 0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    string JsonData;
                    string[] StationType = new string[] { "booster_stores", "gas_stations", "garages", "sport_stores" };

                    JsonData = JsonConvert.SerializeObject(snapshot.Child("booster_stores").GetValue(true));
                    ServerStation.booster_stores = JsonConvert
                        .DeserializeObject<Dictionary<string, Station>>(JsonData);
                    Debug.Log(JsonData);
                    JsonData = JsonConvert.SerializeObject(snapshot.Child("gas_stations").GetValue(true));
                    ServerStation.gas_stations = JsonConvert.DeserializeObject<Dictionary<string, Station>>(JsonData);
                    Debug.Log(JsonData);
                    JsonData = JsonConvert.SerializeObject(snapshot.Child("garages").GetValue(true));
                    ServerStation.garages = JsonConvert.DeserializeObject<Dictionary<string, Station>>(JsonData);
                    Debug.Log(JsonData);
                    JsonData = JsonConvert.SerializeObject(snapshot.Child("sport_stores").GetValue(true));
                    ServerStation.sport_stores = JsonConvert.DeserializeObject<Dictionary<string, Station>>(JsonData);
                    Debug.Log(JsonData);
                    databaseCallback.Invoke("GetServerStation", "Get data success", 0);
                }
            });
        }
        #endregion===========================================Stations===========================================

        #region ===========================================Handler Events===========================================
        void HandleUserChanged(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                DisplayError(args.DatabaseError.Message);
                return;
            }
            DisplayMessage("Client user Changed");
        }

        void HandleMovingRecordChanged(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                DisplayError(args.DatabaseError.Message);
                return;
            }
            // Do something with the data in args.Snapshot
            DisplayMessage("Moving Record changed");
        }

        void HandleVehicleChanged(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                DisplayError(args.DatabaseError.Message);
                return;
            }
            // Do something with the data in args.Snapshot
            DisplayMessage("Client vehicle changed");
        }

        void HandleClientStationChanged(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                DisplayError(args.DatabaseError.Message);
                return;
            }
            // Do something with the data in args.Snapshot
            DisplayMessage("Client station changed");
        }

        void HandleServerStationChanged(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                DisplayError(args.DatabaseError.Message);
                return;
            }
            // Do something with the data in args.Snapshot
            DisplayMessage("ServerStation changed");
        }

        void HandleModelVehicleChanged(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                DisplayError(args.DatabaseError.Message);
                return;
            }
            // Do something with the data in args.Snapshot
            DisplayMessage("Model Vehicle changed");
        }

        #endregion ===========================================Handler Events==========================================

        #region ===========================================DisplayMessage===========================================
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
        #endregion ===========================================DisplayMessage==========================================
    }
}
