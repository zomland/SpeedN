using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FirebaseHandler;
using GUIHandler;

public class PanelStationOnItemControler : MonoBehaviour
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
    float maxEnergyCanFill;
    float maxDurabilityCanRepair;

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
    void ResizeStationContents()
    {
        GUIManager.ResizeScrollRectContent(listStationEnergy);
        GUIManager.ResizeScrollRectContent(listStationRepair);
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
        Invoke("void ResizeStationContents", 0.2f);
    }
    #region ==========================================ShowPanelStation===========================================
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
    #endregion ==========================================ShowPanelStation===========================================

    #region ==========================================FillUp===========================================
    public void LoadFillUpPopUp()
    {
        spriteVehicleOnFillUpPopUp.texture = ClientData.Instance.GetSpriteModelVehicle(vehicle.ModelID).sprite.texture;
        string data;
        amountEnergy = maxEnergyCanFill * fillEnergySlider.value;
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
        else ButtonConfirmFillUp.interactable = true;
        textOnPopUpFillUp.color = Color.white;
        textOnPopUpFillUp.text = data;
    }

    public async void FillUpEnergyVehicle()
    {
        vehicle.FillUpEnergy(amountEnergy);
        myItemSceneUI_2Controller.EnergyMonitorControler.SetValue(vehicle.EnergyPercent());
        ClientData.Instance.ClientUser.ChargeFee(feeEnergy);
        ClientData.Instance.ClientUser.clientVehicle.UpLoadCurrentVehicle();
        coinControler.UpdateCoin();
        chosenStation.ReceiveCoin(feeEnergy);
        ClientData.Instance.ClientUser.clientStation.UpdateNumCoin(chosenStation);
        ServerStation.UpdateNumCoin(chosenStation);
        myItemSceneUI_2Controller.CheckButtonFillAndRepair();
        if (vehicle.EnergyPercent() == 1)
        {
            panelStation.SetActive(false);
            PopupFillEnergy.SetActive(false);
        }
        else PopupFillEnergy.SetActive(false);
        await FirebaseApi.Instance.PostUserValue("numCoin", ClientData.Instance.ClientUser.numCoin, PostDataCallback);
        await FirebaseApi.Instance.PostClientVehicle(PostDataCallback);
        await FirebaseApi.Instance.PostClientStation(chosenStation, PostDataCallback);
        await FirebaseApi.Instance.PostServerStation(chosenStation, PostDataCallback);

    }
    #endregion ==========================================FillUp===========================================

    #region ==========================================Repair===========================================
    public void LoadRepairPopUp()
    {
        spriteVehicleOnRepairPopUp.texture = ClientData.Instance.GetSpriteModelVehicle(vehicle.ModelID).sprite.texture;
        string data;
        amountRepair = maxDurabilityCanRepair * repairSlider.value;
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
        else ButtonConfirmRepair.interactable = true;
        textOnPopUpRepair.color = Color.white;
        textOnPopUpRepair.text = data;
    }

    public async void RepairVehicle()
    {
        vehicle.Repair(amountRepair);
        myItemSceneUI_2Controller.DurabilityMonitorControler.SetValue(vehicle.DurabilityPercent());
        ClientData.Instance.ClientUser.ChargeFee(feeRepair);
        ClientData.Instance.ClientUser.clientVehicle.UpLoadCurrentVehicle();
        coinControler.UpdateCoin();
        chosenStation.ReceiveCoin(feeRepair);
        ClientData.Instance.ClientUser.clientStation.UpdateNumCoin(chosenStation);
        ServerStation.UpdateNumCoin(chosenStation);
        myItemSceneUI_2Controller.CheckButtonFillAndRepair();
        if (vehicle.DurabilityPercent() == 1)
        {
            panelStation.SetActive(false);
            PopupRepair.SetActive(false);
        }
        else PopupRepair.SetActive(false);
        await FirebaseApi.Instance.PostUserValue("numCoin", ClientData.Instance.ClientUser.numCoin, PostDataCallback);
        await FirebaseApi.Instance.PostClientVehicle(PostDataCallback);
        await FirebaseApi.Instance.PostClientStation(chosenStation, PostDataCallback);
        await FirebaseApi.Instance.PostServerStation(chosenStation, PostDataCallback);

    }
    #endregion ==========================================Repair===========================================

    public void ShowPopUpStation()
    {
        if (chosenStation.stationType == StationType.booster_store || chosenStation.stationType == StationType.gas_station)
        {
            fillEnergySlider.value = 1;
            if (vehicle.LostEnergy() * chosenStation.GetPrice() <= ClientData.Instance.ClientUser.numCoin)
                maxEnergyCanFill = vehicle.LostEnergy();
            else maxEnergyCanFill = ClientData.Instance.ClientUser.numCoin / chosenStation.GetPrice();
            LoadFillUpPopUp();
            PopupFillEnergy.SetActive(true);
        }
        else
        {
            repairSlider.value = 1;
            if (vehicle.LostDurability() * chosenStation.GetPrice() <= ClientData.Instance.ClientUser.numCoin)
                maxDurabilityCanRepair = vehicle.LostDurability();
            else maxDurabilityCanRepair = ClientData.Instance.ClientUser.numCoin / chosenStation.GetPrice();
            LoadRepairPopUp();
            PopupRepair.SetActive(true);
        }
    }

    void PostDataCallback(string nameMethod, string message, int errorId = 0)
    {
        Debug.Log("PanelStationControler:" + nameMethod + message + ":" + errorId);
    }


}
