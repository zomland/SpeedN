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


    MyItemSceneUIController myItemSceneUIController;
    MyItemSceneController myItemSceneController;
    MyItemSceneUI_2Controller myItemSceneUI_2Controller;
    Vehicle vehicle;

    void Start()
    {
        myItemSceneUIController = FindObjectOfType<MyItemSceneUIController>();
        myItemSceneController = FindObjectOfType<MyItemSceneController>();
        myItemSceneUI_2Controller = FindObjectOfType<MyItemSceneUI_2Controller>();

        myItemSceneUI_2Controller.EnergyMonitorControler.Initialize(new float[] { 0f, 1f });
        myItemSceneUI_2Controller.DurabilityMonitorControler.Initialize(new float[] { 0f, 1f });
    }

    public void SetProperties(Vehicle _vehicle)
    {
        vehicle = _vehicle;
        spriteVehicle.sprite = ClientData.Instance.GetSpriteModelVehicle(vehicle.Data.ModelID).sprite;
        nameVehicleText.text = vehicle.Data.NameItem;
        tagText.text = vehicle.Data.ItemID;
    }

    public void OnClickButton()
    {
        myItemSceneUIController.OnClickVehicleItem();
        myItemSceneUI_2Controller.DisplayUI(vehicle);
    }

    public void ChooseClick()
    {
        ClientData.Instance.ClientVehicle.currentVehicle = vehicle;
        ClientData.Instance.ClientUser.currentVehicleID = vehicle.Data.ItemID;
        DatabaseHandler.SaveUserData(OnChangeCurrentVehicle);
        Messenger.RaiseMessage(Message.LoadScene, Scenes.HomeScene, Scenes.MyItemScene);
    }

    void OnChangeCurrentVehicle(string message)
    {
        Debug.Log(this.name + "OnChangeCurrentVehicle:Save:");
    }
}
