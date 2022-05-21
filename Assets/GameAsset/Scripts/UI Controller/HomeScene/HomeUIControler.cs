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
    public RawImage currentVehicleRawImg;
    public Text nameVehicleText;


    [Range(0f, 1f)]
    public float valueEnergy;

    void Start()
    {
        LoadEnergyMonitor();
        LoadNameVehicle();
        LoadImageVehicle();
    }


    public void GoToDrivingScene()
    {
        Messenger.RaiseMessage(Message.LoadScene, Scenes.DrivingScene, Scenes.HomeScene);
    }

    // Update is called once per frame

    void LoadImageVehicle()
    {
        currentVehicleRawImg.texture
        = ClientData.Instance.GetSpriteVehicle
        (ClientData.Instance.clientUser.currentVehicle.name).sprite.texture;
    }

    void LoadNameVehicle()
    {
        nameVehicleText.text = ClientData.Instance.clientUser.currentVehicle.name;
    }

    void LoadEnergyMonitor()
    {
        EnergyMonitorControler.SetValueShow(ClientData.Instance.clientUser.currentVehicle.energyPercent());
    }

    /*void Update()
    {
        EnergyMonitorControler.SetValueShow(valueEnergy);
    }*/


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

    #endregion
}
