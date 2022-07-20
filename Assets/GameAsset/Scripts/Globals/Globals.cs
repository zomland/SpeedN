using System.Collections;
using System.Collections.Generic;
using Base.Helper;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

namespace Global
{
    public static class GameStateParam
    {
        public static bool InitializeState = false;
        public static bool LoginState = false;
        public static bool MainState = false;
    }

    public delegate void AuthCallback(FirebaseUser user, string message, AuthError errorId = AuthError.None);

    public delegate void DatabaseCallback(string nameProcedure, string message, int errorId = 0);

    public delegate void GPSPermissionCallback(string permissionName);
    public enum FirebaseError { }


    public enum Message { LoadScene }

    public enum Scenes
    {
        [StringValue("ManagerValue")] ManagerScene = 0,
        [StringValue("LoadingScene")] LoadingScene, [StringValue("LoginScene")] LoginScene,
        [StringValue("HomeScene")] HomeScene, [StringValue("DrivingScene")] DrivingScene,
        [StringValue("AccountScene")] AccountScene, [StringValue("MyItemScene")] MyItemScene,
        [StringValue("MetamaskScene")] MetamaskScene, [StringValue("ImportScene")] ImportScene,
        [StringValue("MarketPlaceScene")] MarketPlaceScene, [StringValue("MyStationScene")] MyStationScene,
        [StringValue("FillFixScene")] FillFixScene
    }
}
