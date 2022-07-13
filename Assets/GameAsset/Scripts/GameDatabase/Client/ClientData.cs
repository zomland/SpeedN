using Base;
using UnityEngine;
using Base.Audio;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Base.Helper;
using System;
using Newtonsoft.Json;


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
        LoadModelVehicleBaseStats();
        LoadUserData();
        LoadClientVehicle();

    }

    void LoadClientVehicle()
    {
        ClientUser.clientVehicle.CreateFromLocal(speedNDefault);
        ClientUser.clientVehicle.InitialLoad(ClientUser.currentVehicleID);
    }

    void LoadModelVehicleBaseStats()
    {
        foreach (var child in speedNDefault.modelVehicleBaseStats)
        {
            ModelVehicle.AddModelStat(child);
        }
    }

    void LoadUserData()
    {

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

    #region =====================================Load&SaveCallback========================================
    void LoadClientVehicleDataCallback(string message)
    {
        Debug.Log("LoadClientVehicleDataCallback: " + message);
    }

    void LoadClientVehicleDataFallback(string message)
    {
        Debug.Log("LoadClientVehicleDataCallback: " + message);
    }

    void LoadClientUserDataCallback(string message)
    {
        Debug.Log("LoadClientUserDataCallback: " + message);
    }

    void LoadClientUserDataFallback(string message)
    {
        Debug.Log("LoadClientUserDataFallback: " + message);
    }

    void LoadClientMovingRecordCallback(string message)
    {
        Debug.Log("LoadClientMovingRecordCallback: " + message);
    }

    void LoadClientMovingRecordFallback(string message)
    {
        Debug.Log("LoadClientMovingRecordFallback: " + message);
    }

    void LoadClientCoinCallback(string message)
    {
        Debug.Log("LoadClientCoinCallback: " + message);
    }

    void LoadClientCoinFallback(string message)
    {
        Debug.Log("LoadClientCoinFallback: " + message);
    }

    void SaveClientCoinCallback(string message)
    {
        Debug.Log("SaveClientCoinFallback: " + message);
    }
    void SaveClientVehicleDataCallback(string message)
    {
        Debug.Log("SaveClientVehicleDataCallback: " + message);
    }

    void SaveClientUserCallback(string message)
    {
        Debug.Log("SaveClientUserDataCallback: " + message);
    }
    #endregion =====================================Load&SaveCallback=====================================

}
