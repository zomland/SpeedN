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
    public ClientUser ClientUser;
    public ClientVehicle ClientVehicle;
    public ClientCoin ClientCoin;
    public ClientMovingRecord ClientMovingRecord;
    [field: SerializeField] public SpeedNDefault speedNDefault { get; private set; }

    void Awake()
    {
        ClientUser = new ClientUser();
        ClientCoin = new ClientCoin();
        ClientVehicle = new ClientVehicle();
        ClientMovingRecord = new ClientMovingRecord();
    }

    #region =====================================LoadData================================================
    public void InitialLoadData()
    {
        LoadClientCoin();
        LoadModelVehicleBaseStats();
        LoadClientVehicleData();
        LoadUserData();
        ClientVehicle.InitialLoad(ClientUser.currentVehicleID);
        DatabaseHandler.LoadMovingRecords(LoadClientMovingRecordCallback, LoadClientMovingRecordFallback);

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
        DatabaseHandler.LoadUserData(LoadClientUserDataCallback, LoadClientUserDataFallback);
        if (ClientUser.currentVehicleID == "null")
        {
            DatabaseHandler.SaveUserData(SaveClientUserCallback);
        }
    }

    void LoadClientVehicleData()
    {
        DatabaseHandler.LoadVehicleData(LoadClientVehicleDataCallback
            , LoadClientVehicleDataFallback);
        if (ClientVehicle.Vehicles.Count == 0)
        {
            ClientVehicle.CreateFromLocal(speedNDefault);
            DatabaseHandler.SaveVehicleData(SaveClientVehicleDataCallback);
        }
    }

    void LoadClientCoin()
    {
        DatabaseHandler.LoadClientCoin(LoadClientCoinCallback
            , LoadClientCoinFallback);
        if (ClientCoin.Coins.Count == 0)
        {
            int numCoin = 777;
            ClientCoin.CreateFromLocal(speedNDefault, numCoin);
            DatabaseHandler.SaveClientCoin(SaveClientCoinCallback);
        }
    }

    #endregion =====================================LoadData==============================================

    #region =====================================Sprite===================================================
    public SpriteIcon GetSpriteIcon(string _spriteID)
    {
        foreach (var child in speedNDefault.spriteIcons)
        {
            if (child.spriteID == _spriteID) return child;
        }
        return null;
    }

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
