using System.Collections;
using System.Collections.Generic;
using Base.MessageSystem;
using Global;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Base.Audio;

public class HomeUIControler : MonoBehaviour
{

    public ClockMonitorControler EnergyMonitorControler;
    public GameObject PopupOutOfEnergy;
    public RawImage currentVehicleRawImg;
    public Text nameVehicleText;
    ClientVehicle _currentVehicle;

    private void Awake()
    {
        _currentVehicle = ClientData.Instance.ClientUser.currentVehicle;
        LoadEnergyMonitor();
        LoadNameVehicle();
        LoadImageVehicle();
    }
    void Start()
    {
        SoundManager.PlayMusic(ClientData.Instance.GetAudioClip(Audio.AudioType.Music, "music001"), 0.3f, true, true);
    }

    void LoadImageVehicle()
    {
        if (_currentVehicle != null)
        {
            currentVehicleRawImg.texture
                = ClientData.Instance.GetSpriteVehicle(_currentVehicle.Attrib.Name).sprite.texture;
        }
    }

    void LoadNameVehicle()
    {
        if (_currentVehicle != null)
        {
            nameVehicleText.text = _currentVehicle.Attrib.Name;
        }
    }

    void LoadEnergyMonitor()
    {
        if (_currentVehicle != null)
        {
            EnergyMonitorControler.Initialize(new float[] { 0f, 1f });
            EnergyMonitorControler.SetValue(_currentVehicle.EnergyPercent());
        }
    }


    #region Button Click Handler

    private void SceneTransferClick(Scenes sceneFrom, Scenes sceneTo)
    {
        Messenger.RaiseMessage(Message.LoadScene, sceneTo, sceneFrom);
    }

    public void ClickToAccountScene()
    {
        SceneTransferClick(Scenes.HomeScene, Scenes.AccountScene);
    }

    public void ClickToItemScene()
    {
        SceneTransferClick(Scenes.HomeScene, Scenes.MyItemScene);
    }

    public void ClickToDrivingScene()
    {
        if (_currentVehicle.IsOutOfEnergy())
        {
            PopupOutOfEnergy.SetActive(true);
        }
        else
        {
            SceneTransferClick(Scenes.HomeScene, Scenes.DrivingScene);
        }
    }

    public void OnButtonClick()
    {
        SoundManager.PlayUISound(ClientData.Instance.GetAudioClip(Audio.AudioType.UISound, "buttonClick01"));
    }

    #endregion
}
