using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FirebaseHandler;
using TMPro;
using Global;
using UnityEngine.SceneManagement;
using System;
using GUIHandler;

public class MyStationSceneControler : MonoBehaviour
{
    [System.Serializable]
    [HideInInspector]
    public struct UIElementGroupByStationType
    {
        public StationType stationType;
        public GameObject panelStation;
        public Button buttonChangeStation;
        public TMP_Text textButton;
    }

    public List<UIElementGroupByStationType> UIElementsGroupByStationType;

    [Header("ListStations")]
    public GameObject listBoosterStore;
    public GameObject listGasStation;
    public GameObject listGarage;
    public GameObject listSportStore;
    [Header("WhereSpawn")]
    public Transform TfSpawn;
    [Header("PopUpSetPrice")]
    public GameObject popupSetPrice;
    public GameObject popupSetPriceSuccess;
    public Text textContentPopupSetPrice;
    public Button buttonConfirmSetPrice;
    public TMP_InputField inputFieldNewPrice;
    [Header("PopUpGetCoin")]
    public GameObject popupGetCoin;
    public GameObject popupGetCoinSuccess;
    public Text textContentPopupGetCoin;
    public Button buttonConfirmGetCoin;
    public Slider sliderGetCoin;

    [Header("Prefabs")]
    public StationGUIPfControler stationGUIPf;
    public AmountCoin coinControler;

    [Header("textMess")]
    public GameObject noItemMess;

    [HideInInspector] public StationGUIPfControler chosenStationGUIControler;
    [HideInInspector] public Station chosenStation;

    float numCoinGetFromStationNoTax;
    float numCoinGetFromStation;

    void Start()
    {
        LoadListsStation();
        ChangeGUIToBoosterStore();
        ResizeContents();
    }

    #region =========================================LoadDataStation=====================================
    public void LoadListsStation()
    {
        ClientStation clientStation = ClientData.Instance.ClientUser.clientStation;
        foreach (var child in clientStation.gas_stations.Values)
        {

            var stationGUI = Instantiate(stationGUIPf, TfSpawn.position, Quaternion.identity, listGasStation.transform);
            stationGUI.LoadStationInfoForMyStation(child);
        }
        foreach (var child in clientStation.garages.Values)
        {

            var stationGUI = Instantiate(stationGUIPf, TfSpawn.position, Quaternion.identity, listGarage.transform);
            stationGUI.LoadStationInfoForMyStation(child);
        }
        foreach (var child in clientStation.booster_stores.Values)
        {
            var stationGUI = Instantiate(stationGUIPf, TfSpawn.position, Quaternion.identity, listBoosterStore.transform);
            stationGUI.LoadStationInfoForMyStation(child);
        }
        foreach (var child in clientStation.sport_stores.Values)
        {
            var stationGUI = Instantiate(stationGUIPf, TfSpawn.position, Quaternion.identity, listSportStore.transform);
            stationGUI.LoadStationInfoForMyStation(child);
        }
    }

    void ResizeContents()
    {
        GUIManager.ResizeScrollRectContent(listBoosterStore);
        GUIManager.ResizeScrollRectContent(listGasStation);
        GUIManager.ResizeScrollRectContent(listGarage);
        GUIManager.ResizeScrollRectContent(listSportStore);
    }
    #endregion =========================================LoadDataStation=====================================

    #region =========================================ChangeUIWithStationType=====================================

    public void ChangeUIElementsWithStationType(StationType _type)
    {
        if (ClientData.Instance.ClientUser.clientStation.NumStation(_type) > 0) noItemMess.SetActive(false);
        else noItemMess.SetActive(true);
        foreach (var child in UIElementsGroupByStationType)
        {
            if (child.stationType == _type)
            {
                child.panelStation.SetActive(true);
                child.buttonChangeStation.image.color = new Color(0f, 1f, 1f);
                child.textButton.color = Color.black;
            }
            else
            {
                child.panelStation.SetActive(false);
                child.buttonChangeStation.image.color = new Color(0.2f, 0.2f, 0.2f);
                child.textButton.color = Color.white;
            }
        }
    }

    public void ChangeGUIToBoosterStore()
    {
        ChangeUIElementsWithStationType(StationType.booster_store);
    }

    public void ChangeGUIToGasStation()
    {
        ChangeUIElementsWithStationType(StationType.gas_station);
    }
    public void ChangeGUIToGarage()
    {
        ChangeUIElementsWithStationType(StationType.garage);
    }
    public void ChangeGUIToSportStore()
    {
        ChangeUIElementsWithStationType(StationType.sport_store);
    }

    #endregion =========================================ChangeUIWithStationType=====================================

    #region =========================================SetPrice=====================================
    public void ShowPopupSetPriceStation()
    {
        inputFieldNewPrice.text = "";
        popupSetPrice.SetActive(true);
    }

    public async void SetPriceStation()
    {
        float newPrice = Convert.ToSingle(inputFieldNewPrice.text);
        chosenStation.SetPrice(newPrice, System.DateTime.Now.ToString());
        ServerStation.UpdatePrice(chosenStation, System.DateTime.Now.ToString());
        chosenStationGUIControler.LoadStationInfoForMyStation();
        popupSetPrice.SetActive(false);
        await FirebaseApi.Instance.PostClientStation(chosenStation, PostDataCallback);
        await FirebaseApi.Instance.PostServerStation(chosenStation, PostDataCallback);
        popupSetPriceSuccess.SetActive(true);
    }
    #endregion =========================================SetPrice=====================================

    #region =========================================GetCoin=====================================
    public void LoadPopupGetCoinFromStation()
    {
        string data;
        numCoinGetFromStationNoTax = chosenStation.numCoin * sliderGetCoin.value;
        data = numCoinGetFromStationNoTax.ToString("0.0") + "/" + chosenStation.numCoin.ToString("0.0") + " coin" + "\n";
        data += "Tax: " + (numCoinGetFromStationNoTax * chosenStation.taxPercent).ToString("0.0")
            + " (" + (chosenStation.taxPercent * 100).ToString("0") + "%)" + "\n";
        numCoinGetFromStation = numCoinGetFromStationNoTax * (1 - chosenStation.taxPercent);
        data += "Coin: " + numCoinGetFromStation.ToString("0.0") + " coin";
        textContentPopupGetCoin.text = data;
        Debug.Log(chosenStation.numCoin);
        if (chosenStation.numCoin > 0) buttonConfirmGetCoin.interactable = true;
        else buttonConfirmGetCoin.interactable = false;

    }
    public void ShowPopupGetCoinFromStation()
    {
        sliderGetCoin.value = 1;
        LoadPopupGetCoinFromStation();
        popupGetCoin.SetActive(true);
    }

    public async void GetCoinFromStation()
    {
        chosenStation.GetCoin(numCoinGetFromStationNoTax);
        ServerStation.UpdateNumCoin(chosenStation);
        chosenStationGUIControler.LoadStationInfoForMyStation();
        ClientData.Instance.ClientUser.ReceiveCoinFromStation(numCoinGetFromStation);
        coinControler.UpdateCoin();
        popupGetCoin.SetActive(false);
        await FirebaseApi.Instance.PostUserValue("numCoin", ClientData.Instance.ClientUser.numCoin, PostDataCallback);
        await FirebaseApi.Instance.PostClientStation(chosenStation, PostDataCallback);
        await FirebaseApi.Instance.PostServerStation(chosenStation, PostDataCallback);
        popupGetCoinSuccess.SetActive(true);
    }
    #endregion =========================================GetCoin=====================================


    void PostDataCallback(string nameMethod, string message, int errorId)
    {
        Debug.Log("MyStationScene:" + nameMethod + message + ":" + errorId);
    }
}
