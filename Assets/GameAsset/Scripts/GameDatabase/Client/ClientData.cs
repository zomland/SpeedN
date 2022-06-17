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
    public ClientUser _clientUser;
    public ClientUser ClientUser => _clientUser;
    public ClientMovingRecord clientMovingRecord;
    [field: SerializeField] public SpeedNDefault speedNDefault { get; private set; }

    void Awake()
    {
        _clientUser = new ClientUser(speedNDefault);
        //Test add Vehicle
        AddVehicle();
        _clientUser.InitialVehicle();
        clientMovingRecord = new ClientMovingRecord();
    }

    public SpriteIcon GetSpriteIcon(string name)
    {
        foreach (var child in speedNDefault.spriteIcons)
        {
            if (child.name == name) return child;
        }
        return null;
    }

    //Vehicle Sprite
    public SpriteVehicle GetSpriteVehicle(string name)
    {
        foreach (var child in speedNDefault.spriteVehicles)
        {
            if (child.name == name) return child;
        }
        return null;
    }

    private void AddVehicle()
    {
        List<Dictionary<string, object>> VehicleDataDicts
            = CSVControler.DataFromCSV("Assets/GameAsset/Scripts/GameDatabase/Client/Vehicle/Data/VehicleDatabase.csv");
        foreach (var child in VehicleDataDicts)
        {
            string Json = JsonConvert.SerializeObject(child);
            VehicleData vehicleData = new VehicleData();
            vehicleData = JsonConvert.DeserializeObject<VehicleData>(Json);
            _clientUser.clientNFT.vehicleControllers.Add(new VehicleController(vehicleData));
        }
    }

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
}
