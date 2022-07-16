using Base;
using UnityEngine;
using Base.Audio;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Base.Helper;
using System;
using Newtonsoft.Json;
using FirebaseHandler;
using Cysharp.Threading.Tasks;
using Global;
using Runtime.Controller;


public class ClientData : Singleton<ClientData>
{
    public ClientUser ClientUser = new ClientUser();
    float percentLoad;
    float numTask = 0;
    float totalTask = 4;
    [field: SerializeField] public SpeedNDefault speedNDefault { get; private set; }

    void Awake()
    {
    }

    #region =====================================LoadData================================================
    public async UniTask InitialLoadData()
    {
        LoginSceneController loginSceneController = FindObjectOfType<LoginSceneController>();
        loginSceneController.ActiveLoadingPage();
        FirebaseApi.Instance.SetUpDatabaseRef();
        await LoadModelVehicle();
        CalculateLoad();
        loginSceneController.showLoadingDataPage(percentLoad);
        await LoadServerStation();
        CalculateLoad();
        loginSceneController.showLoadingDataPage(percentLoad);
        await LoadClientUser();
        CalculateLoad();
        loginSceneController.showLoadingDataPage(percentLoad);
        await LoadClientVehicle();
        CalculateLoad();
        loginSceneController.showLoadingDataPage(percentLoad);
    }

    void CalculateLoad()
    {
        numTask++;
        percentLoad = numTask / totalTask;
    }

    async UniTask LoadClientVehicle()
    {
        if (ClientUser.clientVehicle.currentVehicle.ModelID.Length == 0)
        {
            ClientUser.clientVehicle.CreateFromLocal(speedNDefault);
            await FirebaseApi.Instance.PostClientVehicle(PostDatabaseCallback);
            Debug.Log(JsonConvert.SerializeObject(ClientData.Instance.ClientUser.clientVehicle));
        }
        else ClientUser.clientVehicle.UpLoadCurrentVehicle();
    }

    async UniTask LoadModelVehicle()
    {
        await FirebaseApi.Instance.GetModelVehicle(GetDatabaseCallback);
    }


    async UniTask LoadClientUser()
    {
        await FirebaseApi.Instance.GetUserData(GetDatabaseCallback);
    }

    async UniTask LoadServerStation()
    {
        await FirebaseApi.Instance.GetServerStation(GetDatabaseCallback);
    }

    #endregion =====================================LoadData==============================================

    #region =====================================Sprite===================================================

    //Vehicle Sprite
    public SpriteModelVehicle GetSpriteModelVehicle(string _spriteID)
    {
        foreach (var child in speedNDefault.spriteModelVehicles)
        {
            if (child.spriteID == _spriteID)
                return child;
        }
        return null;
    }
    #endregion =====================================Sprite================================================

    #region =====================================Audio====================================================
    public AudioClip GetAudioClip(Audio.AudioType type, string AudioClipID)
    {
        switch (type)
        {
            case Audio.AudioType.Music:
                foreach (AudioClipBase child in speedNDefault.musicAudioClips)
                {
                    if (child.ID == AudioClipID) return child.clip;
                }
                break;
            case Audio.AudioType.Sound:
                foreach (AudioClipBase child in speedNDefault.soundAudioClips)
                {
                    if (child.ID == AudioClipID) return child.clip;
                }
                break;
            case Audio.AudioType.UISound:
                foreach (AudioClipBase child in speedNDefault.UISoundAudioClips)
                {
                    if (child.ID == AudioClipID) return child.clip;
                }
                break;
            default:
                Debug.Log("Exception AudioType");
                return null;
        }
        Debug.Log("Exception GetAudio: Check Type or ClipID");
        return null;
    }
    #endregion ========================================Audio==============================================

    #region =====================================DatabaseCallback====================================================
    void GetDatabaseCallback(string method, string mess, int id)
    {
        Debug.Log(method + mess + id);
    }
    void PostDatabaseCallback(string method, string mess, int id)
    {
        Debug.Log(method + mess + id);
    }
    #endregion =====================================DatabaseCallback=================================================

}
