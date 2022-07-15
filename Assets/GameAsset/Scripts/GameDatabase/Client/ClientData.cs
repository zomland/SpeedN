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


public class ClientData : Singleton<ClientData>
{
    public ClientUser ClientUser = new ClientUser();
    [field: SerializeField] public SpeedNDefault speedNDefault { get; private set; }

    void Awake()
    {
    }

    #region =====================================LoadData================================================
    public void InitialLoadData()
    {
        FirebaseApi.Instance.SetUpDatabaseRef();
        LoadServerStation();
        LoadModelVehicle();
        LoadClientUser();
    }

    public void LoadClientVehicle()
    {
        if (ClientUser.clientVehicle == null)
        {
            ClientUser.clientVehicle.CreateFromLocal(speedNDefault);
            FirebaseApi.Instance.PostClientVehicle(PostDatabaseCallback).Forget();
        }
    }

    void LoadModelVehicle()
    {
        FirebaseApi.Instance.GetModelVehicle(GetDatabaseCallback).Forget();
    }


    void LoadClientUser()
    {
        FirebaseApi.Instance.GetUserData(GetDatabaseCallback).Forget();
    }

    void LoadServerStation()
    {
        FirebaseApi.Instance.GetServerStation(GetDatabaseCallback).Forget();
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
