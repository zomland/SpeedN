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
    ClientVehicle vehicle;

    void Start()
    {
        myItemSceneUIController = FindObjectOfType<MyItemSceneUIController>();
        myItemSceneController = FindObjectOfType<MyItemSceneController>();
        myItemSceneUI_2Controller= FindObjectOfType<MyItemSceneUI_2Controller>();
    }

    public void SetProperties(ClientVehicle vehicle )
    {
        this.vehicle = vehicle;

        spriteVehicle.sprite = ClientData.Instance.GetSpriteVehicle(vehicle.Attrib.Name).sprite;
        nameVehicleText.text = vehicle.Attrib.Name;
        tagText.text = vehicle.Attrib.ID;
        levelText.text = vehicle.Attrib.Level.ToString();
    }

    public void OnClickButton(){    
        myItemSceneUIController.OnClickVehicleItem();
        myItemSceneUI_2Controller.DisplayUI(vehicle);
    }

    public void ChooseClick()
    {
        ClientData.Instance.ClientUser.currentVehicle = vehicle;
        Messenger.RaiseMessage(Message.LoadScene, Scenes.HomeScene, Scenes.MyItemScene);
    }
}
