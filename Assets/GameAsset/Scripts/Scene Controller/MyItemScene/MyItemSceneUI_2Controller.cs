using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Global;
using FirebaseHandler;

public class MyItemSceneUI_2Controller : MonoBehaviour
{
    [Header("Vehicle")]
    public Image spriteVehicle;
    public TextMeshProUGUI vehicleIDText;
    public TextMeshProUGUI nameText;
    public ClockMonitorControler EnergyMonitorControler;
    public ClockMonitorControler DurabilityMonitorControler;

    [Header("Buttons")]
    public Button ButtonFillUp;
    public Button ButtonRepair;

    [Header("PopUpData")]
    public RawImage spriteVehicleOnFillUpPopUp;
    public RawImage spriteVehicleOnRepairPopUp;
    public Text textOnPopUpFillUp;
    public Text textOnPopUpRepair;
    public Button ButtonConfirmFillUp;
    public Button ButtonConfirmRepair;

    Vehicle Vehicle;
    public float FeeEnergy;
    public float FeeRepair;
    public string UnitFee="SPEEDN";

    public void DisplayUI(Vehicle _Vehicle)
    {
        Vehicle = _Vehicle;
        spriteVehicle.sprite = ClientData.Instance.GetSpriteModelVehicle(Vehicle.ModelID).sprite;
        vehicleIDText.text = Vehicle.ItemID;
        nameText.text = Vehicle.NameItem;
        EnergyMonitorControler.SetValue(Vehicle.EnergyPercent());
        DurabilityMonitorControler.SetValue(Vehicle.DurabilityPercent());
        if (Vehicle.EnergyPercent() == 1) ButtonFillUp.interactable = false;
        else ButtonFillUp.interactable = true;
        if (Vehicle.DurabilityPercent() == 1) ButtonRepair.interactable = false;
        else ButtonRepair.interactable = true;
    }

    public void ResetMonitors()
    {
        EnergyMonitorControler.ResetMonitor();
        DurabilityMonitorControler.ResetMonitor();
    }

    public void LoadFillUpPopUp(float _FeeEnergy)
    {
        spriteVehicleOnFillUpPopUp.texture = ClientData.Instance.GetSpriteModelVehicle(Vehicle.ModelID).sprite.texture;
        string data;
        FeeEnergy = _FeeEnergy;
        FeeEnergy = (1 - Vehicle.EnergyPercent()) * FeeMenu.FeePerEnergy;
        data = "Fee Energy :   " + FeeEnergy.ToString("0.00") + UnitFee + "\n";
        if (!ClientData.Instance.ClientUser.isEnoughCoin(FeeEnergy))
        {
            data += "\n" + "Not enough coin to pay";
            ButtonConfirmFillUp.interactable = false;
            textOnPopUpFillUp.color = Color.red;
        }
        textOnPopUpFillUp.text = data;

    }

    public void LoadRepairPopUp(float _FeeRepair)
    {
        spriteVehicleOnRepairPopUp.texture = ClientData.Instance.GetSpriteModelVehicle(Vehicle.ModelID).sprite.texture;
        string data;
        FeeRepair = _FeeRepair;
        FeeRepair = (1 - Vehicle.DurabilityPercent()) * FeeMenu.FeePerDurability;
        data = "Fee Repair :   " + FeeRepair.ToString("0.00") + UnitFee + "\n";
        if (!ClientData.Instance.ClientUser.isEnoughCoin(FeeRepair))
        {
            data += "\n" + "Not enough coin to pay";
            ButtonConfirmRepair.interactable = false;
            textOnPopUpRepair.color = Color.red;
        }
        textOnPopUpRepair.text = data;

    }

    public void FillUpEnergyVehicle()
    {
        Vehicle.FillUpEnergy();
        EnergyMonitorControler.SetValue(Vehicle.EnergyPercent());
        ClientData.Instance.ClientUser.ChargeFeeFillUp(FeeEnergy);
        DatabaseHandler.SaveClientCoin(callbackSaveData);
        DatabaseHandler.SaveVehicleData(callbackSaveData);
    }

    public void RepairVehicle()
    {
        Vehicle.Repair();
        DurabilityMonitorControler.SetValue(Vehicle.DurabilityPercent());
        ClientData.Instance.ClientUser.ChargeFeeFillUp(FeeRepair);
        DatabaseHandler.SaveClientCoin(callbackSaveData);
        DatabaseHandler.SaveVehicleData(callbackSaveData);
    }

    void callbackFillUp(string a, string b, int c)
    {

    }

    void callbackRepair(string a, string b, int c)
    {

    }

    void callbackSaveData(string message)
    {

    }

}
