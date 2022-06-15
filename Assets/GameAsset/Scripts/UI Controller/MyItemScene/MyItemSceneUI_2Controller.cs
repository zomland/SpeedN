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

    ClientVehicle vehicle;
    float FeeEnergy;
    float FeeRepair;
    float TaxEnergy;
    float TaxRepair;

    public void DisplayUI(ClientVehicle _vehicle)
    {
        vehicle = _vehicle;
        spriteVehicle.sprite = ClientData.Instance.GetSpriteVehicle(vehicle.Attrib.Name).sprite;
        vehicleIDText.text = vehicle.Attrib.ID;
        nameText.text = vehicle.Attrib.Name;
        EnergyMonitorControler.SetValue(vehicle.EnergyPercent());
        DurabilityMonitorControler.SetValue(vehicle.DurabilityPercent());
        levelText.text = vehicle.Attrib.Level.ToString();
        if (vehicle.EnergyPercent() == 1) ButtonFillUp.interactable = false;
        else ButtonFillUp.interactable = true;
        if (vehicle.DurabilityPercent() == 1) ButtonRepair.interactable = false;
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
        spriteVehicleOnFillUpPopUp.texture = ClientData.Instance.GetSpriteVehicle(vehicle.Attrib.Name).sprite.texture;
        string data;
        string UnitFee = " BNB";
        FeeEnergy = (1 - vehicle.EnergyPercent()) * FeeMenu.FeePerEnergy;
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
        spriteVehicleOnRepairPopUp.texture = ClientData.Instance.GetSpriteVehicle(vehicle.Attrib.Name).sprite.texture;
        string data;
        string UnitFee = " BNB";
        FeeRepair = (1 - vehicle.DurabilityPercent()) * FeeMenu.FeePerDurability;
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
        vehicle.FillUpEnergy();
        EnergyMonitorControler.SetValue(vehicle.EnergyPercent());
        ClientData.Instance.ClientUser.UseCoin("BNB", FeeEnergy + TaxEnergy);
        List<ClientCoin> newClientCoin = ClientData.Instance.ClientUser.clientCoins;
        FirebaseApi.Instance.PostUserValue("clientCoins", newClientCoin, callbackFillUp).Forget();
        List<ClientVehicle> newClientVehicles = ClientData.Instance.ClientUser.clientNFT.clientVehicles;
        FirebaseApi.Instance.PostUserNFT(newClientVehicles,TypeNFT.Vehicle,callbackFillUp).Forget();
    }

    public void RepairVehicle()
    {
        vehicle.Repair();
        DurabilityMonitorControler.SetValue(vehicle.DurabilityPercent());
        ClientData.Instance.ClientUser.UseCoin("BNB", FeeRepair + TaxRepair);
        ClientData.Instance.ClientUser.UseCoin("BNB", FeeEnergy + TaxEnergy);
        List<ClientCoin> newClientCoin = ClientData.Instance.ClientUser.clientCoins;
        FirebaseApi.Instance.PostUserValue("clientCoins", newClientCoin, callbackRepair).Forget();
        List<ClientVehicle> newClientVehicles = ClientData.Instance.ClientUser.clientNFT.clientVehicles;
        FirebaseApi.Instance.PostUserNFT(newClientVehicles,TypeNFT.Vehicle,callbackFillUp).Forget();
    }

    void callbackFillUp(string a, string b, int c)
    {

    }

    void callbackRepair(string a, string b, int c)
    {

    }

}
