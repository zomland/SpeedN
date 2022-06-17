using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Global;
using FirebaseHandler;
using Global;

public class MyItemSceneUI_2Controller : MonoBehaviour
{
    [Header("Vehicle")]
    public Image spriteVehicle;
    public TextMeshProUGUI vehicleIDText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
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

    VehicleController vehicleController;
    float FeeEnergy;
    float FeeRepair;
    float TaxEnergy;
    float TaxRepair;

    public void DisplayUI(VehicleController _vehicleController)
    {
        vehicleController = _vehicleController;
        spriteVehicle.sprite = ClientData.Instance.GetSpriteVehicle(vehicleController.data.name).sprite;
        vehicleIDText.text = vehicleController.data.TokenId;
        nameText.text = vehicleController.data.name;
        EnergyMonitorControler.SetValue(vehicleController.EnergyPercent());
        DurabilityMonitorControler.SetValue(vehicleController.DurabilityPercent());
        levelText.text = vehicleController.data.Level.ToString();
        if (vehicleController.EnergyPercent() == 1) ButtonFillUp.interactable = false;
        else ButtonFillUp.interactable = true;
        if (vehicleController.DurabilityPercent() == 1) ButtonRepair.interactable = false;
        else ButtonRepair.interactable = true;
        LoadFillUpPopUp();
        LoadRepairPopUp();
    }

    public void ResetMonitors()
    {
        EnergyMonitorControler.ResetMonitor();
        DurabilityMonitorControler.ResetMonitor();
    }

    public void LoadFillUpPopUp()
    {
        spriteVehicleOnFillUpPopUp.texture = ClientData.Instance.GetSpriteVehicle(vehicleController.data.name).sprite.texture;
        string data;
        string UnitFee = " BNB";
        FeeEnergy = (1 - vehicleController.EnergyPercent()) * FeeMenu.FeePerEnergy;
        TaxEnergy = FeeEnergy * FeeMenu.TaxPercent;
        data = "Fee Energy :   " + FeeEnergy.ToString("0.00") + UnitFee + "\n";
        data += "Tax fee:       " + TaxEnergy.ToString("0.00") + UnitFee;
        if (!ClientData.Instance.ClientUser.isEnoughCoin("BNB", FeeEnergy))
        {
            data += "\n" + "Not enough coin to pay";
            ButtonConfirmFillUp.interactable = false;
            textOnPopUpFillUp.color = Color.red;
        }
        textOnPopUpFillUp.text = data;
        textOnPopUpFillUp.color = Color.red;

    }

    public void LoadRepairPopUp()
    {
        spriteVehicleOnRepairPopUp.texture = ClientData.Instance.GetSpriteVehicle(vehicleController.data.name).sprite.texture;
        string data;
        string UnitFee = " BNB";
        FeeRepair = (1 - vehicleController.DurabilityPercent()) * FeeMenu.FeePerDurability;
        TaxRepair = FeeRepair * FeeMenu.TaxPercent;
        data = "Fee Repair :   " + FeeRepair.ToString("0.00") + UnitFee + "\n";
        data += "Tax fee:       " + TaxRepair.ToString("0.00") + UnitFee;
        if (!ClientData.Instance.ClientUser.isEnoughCoin("BNB", FeeRepair))
        {
            data += "\n" + "Not enough coin to pay";
            ButtonConfirmRepair.interactable = false;
            textOnPopUpRepair.color = Color.red;
        }
        textOnPopUpRepair.text = data;

    }

    public void FillUpEnergyVehicle()
    {
        vehicleController.FillUpEnergy();
        EnergyMonitorControler.SetValue(vehicleController.EnergyPercent());
        ClientData.Instance.ClientUser.UseCoin("BNB", FeeEnergy + TaxEnergy);
        List<ClientCoin> newClientCoin = ClientData.Instance.ClientUser.clientCoins;
        FirebaseApi.Instance.PostUserValue("clientCoins", newClientCoin, callbackFillUp).Forget();
        List<VehicleController> newClientVehicles = ClientData.Instance.ClientUser.clientNFT.vehicleControllers;
        FirebaseApi.Instance.PostUserNFT(newClientVehicles,TypeNFT.Vehicle,callbackFillUp).Forget();
    }

    public void RepairVehicle()
    {
        vehicleController.Repair();
        DurabilityMonitorControler.SetValue(vehicleController.DurabilityPercent());
        ClientData.Instance.ClientUser.UseCoin("BNB", FeeRepair + TaxRepair);
        ClientData.Instance.ClientUser.UseCoin("BNB", FeeEnergy + TaxEnergy);
        List<ClientCoin> newClientCoin = ClientData.Instance.ClientUser.clientCoins;
        FirebaseApi.Instance.PostUserValue("clientCoins", newClientCoin, callbackRepair).Forget();
        List<VehicleController> newClientVehicles = ClientData.Instance.ClientUser.clientNFT.vehicleControllers;
        FirebaseApi.Instance.PostUserNFT(newClientVehicles,TypeNFT.Vehicle,callbackFillUp).Forget();
    }

    void callbackFillUp(string a, string b, int c)
    {

    }

    void callbackRepair(string a, string b, int c)
    {

    }

}
