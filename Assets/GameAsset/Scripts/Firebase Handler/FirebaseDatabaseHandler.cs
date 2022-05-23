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
        DatabaseReference databaseUsersRef;
        DatabaseReference databaseAssetsRef;

        public DatabaseCallback ValueChangedCallBack,ValueChangedFallBack;

        ~FirebaseDatabaseHandler()
        {

        }

        public void InitializeDatabase()
        {
            databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
            databaseUsersRef = FirebaseDatabase.DefaultInstance.GetReference("Users");
            databaseAssetsRef = FirebaseDatabase.DefaultInstance.GetReference("Assets");
            databaseUsersRef.ValueChanged += HandleValueChanged;
            databaseUsersRef.ChildAdded += HandleChildAdded;
            databaseUsersRef.ChildChanged += HandleChildChanged;
            databaseUsersRef.ChildRemoved += HandleChildRemoved;
            databaseUsersRef.ChildMoved += HandleChildMoved;
        }

        public async UniTaskVoid PostUser(ClientUser user, DatabaseCallback databaseCallback)
        {
            await databaseUsersRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                bool isUserExisted = false;
                if (task.IsFaulted)
                {
                    //DisplayError("connect database failed");
                    databaseCallback.Invoke("PostUser","connect database failed",0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    DataSnapshot UserSnapshot = snapshot.Child(user.userKey);
                    if (UserSnapshot.GetValue(true) == null)
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
                    string JsonData = JsonUtility.ToJson(user);
                    databaseUsersRef.Child(user.userKey).SetRawJsonValueAsync(JsonData);
                    //DisplayWarning("PostUser: post success");
                    databaseCallback?.Invoke("PostUser","success",0); 
                }
                else
                {
                    //DisplayWarning("Post User: User not existed");
                    databaseCallback.Invoke("PostUser","User not existed",0);
                }
            });   
        }

        public async UniTaskVoid PostUserValue(ClientUser user, string valueKey, System.Object newValue, DatabaseCallback databaseCallback)
        {
            await databaseUsersRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                bool isUserExisted = false;
                if (task.IsFaulted)
                {
                    //DisplayError("connect database failed");
                    databaseCallback.Invoke("PostUserValue","connect database failed",0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    DataSnapshot UserSnapshot = snapshot.Child(user.userKey);
                    if (UserSnapshot.GetValue(true) == null)
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
                    databaseUsersRef.Child(user.userKey).Child(valueKey).SetValueAsync(newValue);
                    databaseCallback.Invoke("PostUserValue","success",0); 
                }
                else
                {
                    string JsonData = user.GetStringJsonData();
                    databaseUsersRef.Child(user.userKey).SetRawJsonValueAsync(JsonData);
                    databaseCallback.Invoke("PostUserValue","User not existed",0);
                }
            });
            
        }

        public async UniTaskVoid AddNewUser(ClientUser user, DatabaseCallback databaseCallback)
        {
            await databaseUsersRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                bool isUserExisted = false;
                if (task.IsFaulted)
                {
                    //DisplayError("connect database failed");
                    databaseCallback.Invoke("AddNewUser","connect database failed",0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    DataSnapshot UserSnapshot = snapshot.Child(user.userKey);
                    if (UserSnapshot.GetValue(true) == null)
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
                    //DisplayWarning("AddUser: user existed");
                    databaseCallback.Invoke("AddNewUser","User existed",0);
                }
                else
                {
                    string JsonData = user.GetStringJsonData();
                    databaseUsersRef.Child(user.userKey).SetRawJsonValueAsync(JsonData);
                    //DisplayWarning("AddUser: Add success");
                    databaseCallback.Invoke("AddNewUser","Add success",0);
                }
            });

        }

        public async UniTaskVoid GetUserData(ClientUser user, DatabaseCallback databaseCallback)
        {
            await databaseUsersRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    //DisplayError("Get data user failed");
                    databaseCallback.Invoke("GetUser","connect database failed",0);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    DataSnapshot UserSnapshot = snapshot.Child(user.userKey);
                    string JsonData = JsonConvert.SerializeObject(UserSnapshot.GetValue(true));
                    JsonUtility.FromJsonOverwrite(JsonData, user);
                    Debug.Log(JsonData);
                    //DisplayMessage("Get data success");
                    databaseCallback.Invoke("GetUser","Get data success",0);
                }
            });
        }

        public void RemoveUser(ClientUser user, DatabaseCallback databaseCallback)
        {
            //Delete Account
            databaseUsersRef.Child(user.userKey).SetValueAsync(null);
            //DisplayWarning("User is removed");
            databaseCallback.Invoke("RemoveUser","User is removed",0);
        }

        public async UniTaskVoid CheckUserExisted(ClientUser user, DatabaseCallback databaseCallback)
        {
            await  databaseUsersRef
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                bool isUserExisted = false;
                if (task.IsFaulted)
                {
                    //DisplayError("Check data user failed: cant connect database");
                    databaseCallback.Invoke("CheckUserExisted","connect database failed",0);
                    isUserExisted = false;
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    // Do something with snapshot...
                    DataSnapshot UserSnapshot = snapshot.Child(user.userKey);
                    if (UserSnapshot.GetValue(true) == null)
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
                    databaseCallback.Invoke("CheckUserExisted","User existed",0);
                }
                else
                {
                    databaseCallback.Invoke("CheckUserExisted","username does not exist -> new user or error",0);
                }
            });

        }

        public void SetValueChangedCallBack(DatabaseCallback OnValueChanged)
        {
            ValueChangedCallBack= OnValueChanged;
        }

        public void SetUpDatabaseChangeHandler()
        {
            
        }

        void HandleValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                DisplayError(args.DatabaseError.Message);
                return;
            }
            // Do something with the data in args.Snapshot
            //ValueChangedCallBack.Invoke("HandleValueChanged","value has changed",0);
        }

        void HandleChildAdded(object sender, ChildChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                DisplayError(args.DatabaseError.Message);
                return;
            }
            // Do something with the data in args.Snapshot
            DisplayMessage("User.ChildAdded");
        }

        void HandleChildChanged(object sender, ChildChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                Debug.LogError(args.DatabaseError.Message);
                return;
            }
            // Do something with the data in args.Snapshot
            DisplayMessage("User.ChildChanged");
        }

        void HandleChildRemoved(object sender, ChildChangedEventArgs args)
        {
            if (args.DatabaseError != null)
            {
                Debug.LogError(args.DatabaseError.Message);
                return;
            }
            // Do something with the data in args.Snapshot
            DisplayMessage("User.ChildRemove");
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


    }
}
