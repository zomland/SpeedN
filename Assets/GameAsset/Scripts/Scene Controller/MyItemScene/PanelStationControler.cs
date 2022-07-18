using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FirebaseHandler;

public class PanelStationControler : MonoBehaviour
{
    [HideInInspector] public Vehicle vehicle;
    [HideInInspector] public Station chosenStation;
    public TMP_Text textTitlePanel;
    public GameObject listStationEnergy;
    public GameObject listStationRepair;
    public Transform TfSpawn;
    public StationGUIPfControler stationGUIPf;
    public AmountCoin coinControler;

    [Header("PanelStation")]
    public GameObject panelStation;
    public GameObject panelStationEnergy;
    public GameObject panelStationRepair;

    [Header("PopUpEnergy&Repair")]
    public GameObject PopupFillEnergy;
    public GameObject PopupRepair;
    public RawImage spriteVehicleOnFillUpPopUp;
    public RawImage spriteVehicleOnRepairPopUp;
    public Slider fillEnergySlider;
    public Slider repairSlider;
    public Text textOnPopUpFillUp;
    public Text textOnPopUpRepair;
    public Button ButtonConfirmFillUp;
    public Button ButtonConfirmRepair;

    [Header("SceneControler")]
    public MyItemSceneUI_2Controller myItemSceneUI_2Controller;


    enum PanelStationType { Energy, Repair }
    string UnitFee = "coin";
    string energyType;
    float amountEnergy;
    float feeEnergy;
    float amountRepair;
    float feeRepair;

    void LoadTitlePanelStation(PanelStationType _panelStationType)
    {
        if (_panelStationType == PanelStationType.Energy)
        {
            if (vehicle.NftType == NFTType.Car || vehicle.NftType == NFTType.Motorbike) textTitlePanel.text = "Gas Station";
            else textTitlePanel.text = "Booster Store";
        }
        else
        {
            if (vehicle.NftType == NFTType.Car || vehicle.NftType == NFTType.Motorbike) textTitlePanel.text = "Garage";
            else textTitlePanel.text = "Sport Store";
        }
    }

    public void LoadListsStation(Vehicle _vehicle)
    {
        //Chua check null owner(tram khong co nguoi so huu)
        vehicle = _vehicle;
        if (vehicle.NftType == NFTType.Car || vehicle.NftType == NFTType.Motorbike)
        {
            Debug.Log("LoadListsStation");
            foreach (var child in ServerStation.gas_stations.Values)
            {
                var station = Instantiate(stationGUIPf, TfSpawn.position, Quaternion.identity, listStationEnergy.transform);
                station.LoadStationInfoForFillAndRepair(child);
                Debug.Log("LoadListsStation " + child.stationID);
            }
            foreach (var child in ServerStation.garages.Values)
            {
                var station = Instantiate(stationGUIPf, TfSpawn.position, Quaternion.identity, listStationRepair.transform);
                station.LoadStationInfoForFillAndRepair(child);
                Debug.Log("LoadListsStation " + child.stationID);
            }
        }
        else
        {
            foreach (var child in ServerStation.booster_stores.Values)
            {
                var station = Instantiate(stationGUIPf, TfSpawn.position, Quaternion.identity, listStationEnergy.transform);
                station.LoadStationInfoForFillAndRepair(child);
            }
            foreach (var child in ServerStation.sport_stores.Values)
            {
                var station = Instantiate(stationGUIPf, TfSpawn.position, Quaternion.identity, listStationRepair.transform);
                station.LoadStationInfoForFillAndRepair(child);
            }
        }
    }

    public void ShowPanelStationEnergy()
    {
        LoadTitlePanelStation(PanelStationType.Energy);
        panelStation.SetActive(true);
        panelStationEnergy.SetActive(true);
        panelStationRepair.SetActive(false);
    }

    public void ShowPanelStationRepair()
    {
        LoadTitlePanelStation(PanelStationType.Repair);
        panelStation.SetActive(true);
        panelStationEnergy.SetActive(false);
        panelStationRepair.SetActive(true);
    }

    public void LoadFillUpPopUp()
    {
        spriteVehicleOnFillUpPopUp.texture = ClientData.Instance.GetSpriteModelVehicle(vehicle.ModelID).sprite.texture;
        string data;
        amountEnergy = vehicle.LostEnergy() * fillEnergySlider.value;
        feeEnergy = amountEnergy * chosenStation.GetPrice();
        data = "Fee Energy :   " + feeEnergy.ToString("0.00") + " " + UnitFee + "\n";
        data += "New" + vehicle.EnergyName() + ":   " + (amountEnergy + vehicle.Energy).ToString("0") + "/"
            + vehicle.ModelStats().EnergyMax.ToString("0") + "\n";
        if (!ClientData.Instance.ClientUser.isEnoughCoin(feeEnergy))
        {
            data += "\n" + "Not enough coin to pay";
            ButtonConfirmFillUp.interactable = false;
            textOnPopUpFillUp.color = Color.red;
        }
        textOnPopUpFillUp.color = Color.white;
        textOnPopUpFillUp.text = data;
    }

    public void LoadRepairPopUp()
    {
        spriteVehicleOnRepairPopUp.texture = ClientData.Instance.GetSpriteModelVehicle(vehicle.ModelID).sprite.texture;
        string data;
        amountRepair = vehicle.LostDurability() * repairSlider.value;
        feeRepair = amountRepair * chosenStation.GetPrice();
        data = "Fee Repair :   " + feeRepair.ToString("0.00") + " " + UnitFee + "\n";
        data += "New Durability :   " + (amountRepair + vehicle.Durability).ToString("0") + "/"
            + vehicle.ModelStats().DurabilityMax.ToString("0") + "\n";
        if (!ClientData.Instance.ClientUser.isEnoughCoin(feeRepair))
        {
            data += "\n" + "Not enough coin to pay";
            ButtonConfirmRepair.interactable = false;
            textOnPopUpRepair.color = Color.red;
        }
        textOnPopUpRepair.color = Color.white;
        textOnPopUpRepair.text = data;
    }

    public async void FillUpEnergyVehicle()
    {
        vehicle.FillUpEnergy(amountEnergy);
        myItemSceneUI_2Controller.EnergyMonitorControler.SetValue(vehicle.EnergyPercent());
        ClientData.Instance.ClientUser.ChargeFee(feeEnergy);
        ClientData.Instance.ClientUser.clientVehicle.UpLoadCurrentVehicle();
        coinControler.UpdateCoin();
        myItemSceneUI_2Controller.CheckButtonFillAndRepair();
        if (vehicle.EnergyPercent() == 1)
        {
            panelStation.SetActive(false);
            PopupFillEnergy.SetActive(false);
        }
        else PopupFillEnergy.SetActive(false);
        await FirebaseApi.Instance.PostUserValue("numCoin", ClientData.Instance.ClientUser.numCoin, PostDataCallback);
        await FirebaseApi.Instance.PostClientVehicle(PostDataCallback);

    }

    public async void RepairVehicle()
    {
        vehicle.Repair(amountRepair);
        myItemSceneUI_2Controller.DurabilityMonitorControler.SetValue(vehicle.DurabilityPercent());
        ClientData.Instance.ClientUser.ChargeFee(feeRepair);
        ClientData.Instance.ClientUser.clientVehicle.UpLoadCurrentVehicle();
        coinControler.UpdateCoin();
        myItemSceneUI_2Controller.CheckButtonFillAndRepair();
        if (vehicle.DurabilityPercent() == 1)
        {
            panelStation.SetActive(false);
            PopupRepair.SetActive(false);
        }
        else PopupRepair.SetActive(false);
        await FirebaseApi.Instance.PostUserValue("numCoin", ClientData.Instance.ClientUser.numCoin, PostDataCallback);
        await FirebaseApi.Instance.PostClientVehicle(PostDataCallback);
    }

    public void ShowPopUpStation()
    {
        if (chosenStation.stationType == StationType.booster_store || chosenStation.stationType == StationType.gas_station)
        {
            fillEnergySlider.value = 1;
            LoadFillUpPopUp();
            PopupFillEnergy.SetActive(true);
        }
        else
        {
            repairSlider.value = 1;
            LoadRepairPopUp();
            PopupRepair.SetActive(true);
        }
    }

    void PostDataCallback(string nameMethod, string message, int errorId = 0)
    {
        Debug.Log("PanelStationControler:" + nameMethod + message + ":" + errorId);
    }


}
