using System.Collections;
using System.Collections.Generic;
using FirebaseHandler;
using UnityEngine;
using Global;
using Base.MessageSystem;
using UnityEngine.SceneManagement;
using Global;

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

    public void GotoItemFromHomeScene()
    {
        SceneManager.LoadScene(Scenes.HomeScene.ToString(), LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(Scenes.MyItemScene.ToString());
    }
}
