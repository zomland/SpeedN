using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Base.MessageSystem;
using Global;

public class MyItemSceneItemVehicle : MonoBehaviour
{
    public Image spriteVehicle;
    public TextMeshProUGUI nameVehicleText;
    public TextMeshProUGUI tagText;
    public TextMeshProUGUI levelText;


    MyItemSceneUIController myItemSceneUIController;
    MyItemSceneController myItemSceneController;
    MyItemSceneUI_2Controller myItemSceneUI_2Controller;
    VehicleController vehicleController;

    void Start()
    {
        myItemSceneUIController = FindObjectOfType<MyItemSceneUIController>();
        myItemSceneController = FindObjectOfType<MyItemSceneController>();
        myItemSceneUI_2Controller= FindObjectOfType<MyItemSceneUI_2Controller>();

        myItemSceneUI_2Controller.EnergyMonitorControler.Initialize(new float[]{0f,1f});
        myItemSceneUI_2Controller.DurabilityMonitorControler.Initialize(new float[]{0f,1f});
    }

    public void SetProperties(VehicleController _vehicleController )
    {
        vehicleController=_vehicleController;
        spriteVehicle.sprite = ClientData.Instance.GetSpriteVehicle(vehicleController.data.name).sprite;
        nameVehicleText.text = vehicleController.data.name;
        tagText.text = vehicleController.data.TokenId;
        levelText.text = vehicleController.data.Level.ToString();
    }

    public void OnClickButton(){    
        myItemSceneUIController.OnClickVehicleItem();
        myItemSceneUI_2Controller.DisplayUI(vehicleController);
    }

    public void ChooseClick()
    {
        ClientData.Instance.ClientUser.currentVehicleController = vehicleController;
        Messenger.RaiseMessage(Message.LoadScene, Scenes.HomeScene, Scenes.MyItemScene);
    }
}
