using System.Collections;
using System.Collections.Generic;
using FirebaseHandler;
using UnityEngine;
using Global;
using Base.MessageSystem;

public class PopUpControler : MonoBehaviour
{
    public void CheckGPS()
    {
        Debug.LogWarning("Please check GPS");
    }

    public void LogOut()
    {
        FirebaseApi.Instance.SignOut();
        GameStateParam.LoginState = true;
    }

    public void StopDriving()
    {
        //Messenger.RaiseMessage(Message.LoadScene, Scenes.HomeScene, Scenes.DrivingScene);
        Debug.LogWarning("Stop driving");
    }
}
