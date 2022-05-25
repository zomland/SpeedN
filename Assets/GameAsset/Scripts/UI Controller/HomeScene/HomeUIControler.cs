using System.Collections;
using System.Collections.Generic;
using Base.MessageSystem;
using Global;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeUIControler : MonoBehaviour
{

    public ClockMonitorControler EnergyMonitorControler;
    public GameObject PopupOutOfEnergy;
    public RawImage currentVehicleRawImg;
    public Text nameVehicleText;
    ClientVehicle _currentVehicle;

    private void Awake()
    {
        Debug.Log("home scene START");
        _currentVehicle = ClientData.Instance.ClientUser.currentVehicle;
        LoadEnergyMonitor();
        LoadNameVehicle();
        LoadImageVehicle();
    }
    void Start()
    {

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

    #endregion
}
