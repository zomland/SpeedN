using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Base.MessageSystem;
using Global;
using UnityEngine.SceneManagement;
using Base.Helper;
using FirebaseHandler;

public class MyItemSceneItemVehicle : MonoBehaviour
{
    public Image spriteVehicle;
    public TextMeshProUGUI nameVehicleText;
    public TextMeshProUGUI tagText;

    [SerializeField] private Text durabilityText;
    [SerializeField] private Text efficiencyText;
    [SerializeField] private Text energyText;

    [Header("SceneControler")]
    MyItemSceneUIController myItemSceneUIController;
    MyItemSceneController myItemSceneController;
    MyItemSceneUI_2Controller myItemSceneUI_2Controller;

    [Header("PanelControler")]
    PanelStationControler panelStationControler;


    Vehicle vehicle;

    void Start()
    {
        myItemSceneUIController = FindObjectOfType<MyItemSceneUIController>();
        myItemSceneController = FindObjectOfType<MyItemSceneController>();
        myItemSceneUI_2Controller = FindObjectOfType<MyItemSceneUI_2Controller>();
        panelStationControler = FindObjectOfType<PanelStationControler>();


        myItemSceneUI_2Controller.EnergyMonitorControler.Initialize(new float[] { 0f, 1f });
        myItemSceneUI_2Controller.DurabilityMonitorControler.Initialize(new float[] { 0f, 1f });

        durabilityText.text = $"Durability: \n{vehicle.DurabilityPercent() * 100}" + "%";
        efficiencyText.text = $"Efficiency: \n{vehicle.ModelStats().Efficiency}";
        string energyTxt = String.Empty;
        if (vehicle.ModelStats().NftType == NFTType.Bicycle) energyTxt = "Stamina:";
        else if (vehicle.ModelStats().NftType == NFTType.Shoes) energyTxt = "Stamina:";
        else if (vehicle.ModelStats().NftType == NFTType.Car) energyTxt = "Gas:";

        energyText.text = $"{energyTxt} \n{vehicle.EnergyPercent() * 100}" + "%";
    }

    public void SetProperties(Vehicle _vehicle)
    {
        vehicle = _vehicle;
        spriteVehicle.sprite = ClientData.Instance.GetSpriteModelVehicle(vehicle.ModelID).sprite;
        nameVehicleText.text = vehicle.NameItem;
        tagText.text = vehicle.ItemID;
    }

    public void OnClickButton()
    {
        myItemSceneUIController.OnClickVehicleItem();
        myItemSceneUI_2Controller.DisplayUI(vehicle);
        panelStationControler.LoadListsStation(vehicle);
    }


    public async void ChooseClick()
    {
        ClientData.Instance.ClientUser.clientVehicle.currentVehicle = vehicle;
        SceneManager.LoadScene(Scenes.HomeScene.ToString(), LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(Scenes.MyItemScene.ToString());
        await FirebaseApi.Instance.PostClientVehicle(PostDatabaseCallback);
    }
    void PostDatabaseCallback(string method, string mess, int id)
    {
        Debug.Log(method + mess + id);
    }

}
