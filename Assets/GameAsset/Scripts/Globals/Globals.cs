using System.Collections;
using System.Collections.Generic;
using Base.Helper;
using Firebase.Auth;
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

    public delegate void DatabaseCallback(string nameProcedure, string message, int DatabaseErrorID = 0);
    public enum FirebaseError { }

    public enum DatabaseErrorID
    {
        None = 0, Disconnected = -4, ExpiredToken = -6, InvalidToken = -7, MaxRetries = -8,
        NetworkError = -24, OperationFailed = -2, OverriddenBySet = -9, PermissionDenied = -3,
        Unavailable = -10, UnknownError = -999, UserCodeException = -11,
        WriteCanceled = -25
    }

    public enum Message { LoadScene }

    public enum Scenes
    {
        [StringValue("ManagerValue")] ManagerScene = 0,
        [StringValue("LoadingScene")] LoadingScene, [StringValue("LoginScene")] LoginScene,
        [StringValue("HomeScene")] HomeScene, [StringValue("DrivingScene")] DrivingScene,
        [StringValue("AccountScene")] AccountScene, [StringValue("MyItemScene")] MyItemScene
    }
}
