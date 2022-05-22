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
        ClientVehicle crrVehicle = ClientData.Instance.ClientUser.currentVehicle;
        if (crrVehicle != null)
        {
            currentVehicleRawImg.texture = ClientData.Instance.GetSpriteVehicle(crrVehicle.name).sprite.texture;
        }
    }

    void LoadNameVehicle()
    {
        ClientVehicle crrVehicle = ClientData.Instance.ClientUser.currentVehicle;
        if (crrVehicle != null)
        {
            nameVehicleText.text = crrVehicle.name;
        }
        
    }

    void LoadEnergyMonitor()
    {
        ClientVehicle crrVehicle = ClientData.Instance.ClientUser.currentVehicle;
        if (crrVehicle != null) EnergyMonitorControler.SetValueShow(crrVehicle.energyPercent());
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
