using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FirebaseHandler;
using TMPro;
using Global;
using UnityEngine.SceneManagement;
using GUIHandler;

public class MarketPlaceSceneControler : MonoBehaviour
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
    public enum MarketState
    {
        Station, Vehicle
    }

    [System.Serializable]
    [HideInInspector]
    public struct UIElementGroupByMarketState
    {
        public MarketState stationType;
        public GameObject panel;
        public Button buttonChangeMarketState;
        public RawImage imageButton;
    }

    [System.Serializable]
    [HideInInspector]
    public struct UIElementGroupByVehicleType
    {
        public NFTType VehicleType;
        public GameObject panelStation;
        public Button buttonChangeStation;
        public TMP_Text textButton;
    }

    public List<UIElementGroupByMarketState> UIElementsGroupByMarketState;
    public List<UIElementGroupByStationType> UIElementsGroupByStationType;
    public List<UIElementGroupByVehicleType> UIElementsGroupByVehicleType;

    [Header("ListStations")]
    public GameObject listBoosterStore;
    public GameObject listGasStation;
    public GameObject listGarage;
    public GameObject listSportStore;
    [Header("WhereSpawn")]
    public Transform TfSpawn;
    [Header("PopUp")]
    public GameObject popupBidding;
    public GameObject popupBiddingSuccess;
    public Text textContentPopupBidding;
    public Button buttonConfirmBidding;
    public Button buttonToImportScene;
    [Header("Prefabs")]
    public StationGUIPfControler stationGUIPf;
    public AmountCoin coinControler;
    [Header("textMess")]
    public GameObject noItemMessStation;
    public GameObject noItemMessVehicle;


    [HideInInspector] public StationGUIPfControler chosenStationGUIControler;
    [HideInInspector] public Station chosenStation;

    string dateBidding;

    float feeBiding = 300;
    // Start is called before the first frame update
    void Start()
    {
        LoadListsStation();
        ChangUIToStationState();
        ChangeGUIToBoosterStore();
        ChangeGUIToShoes();
        ResizeStationContents();
    }
    void ResizeStationContents()
    {
        GUIManager.ResizeScrollRectContent(listBoosterStore);
        GUIManager.ResizeScrollRectContent(listGasStation);
        GUIManager.ResizeScrollRectContent(listGarage);
        GUIManager.ResizeScrollRectContent(listSportStore);
    }

    #region =========================================LoadDataStation=====================================
    public void LoadListsStation()
    {
        foreach (var child in ServerStation.gas_stations.Values)
        {
            if (child.ownerID.Length == 0)
            {
                var stationGUI = Instantiate(stationGUIPf, TfSpawn.position, Quaternion.identity, listGasStation.transform);
                stationGUI.LoadStationInfoForBidding(child);
            }
        }
        foreach (var child in ServerStation.garages.Values)
        {
            if (child.ownerID.Length == 0)
            {
                var stationGUI = Instantiate(stationGUIPf, TfSpawn.position, Quaternion.identity, listGarage.transform);
                stationGUI.LoadStationInfoForBidding(child);
            }
        }
        foreach (var child in ServerStation.booster_stores.Values)
        {
            if (child.ownerID.Length == 0)
            {
                var stationGUI = Instantiate(stationGUIPf, TfSpawn.position, Quaternion.identity, listBoosterStore.transform);
                stationGUI.LoadStationInfoForBidding(child);
            }
        }
        foreach (var child in ServerStation.sport_stores.Values)
        {
            if (child.ownerID.Length == 0)
            {
                var stationGUI = Instantiate(stationGUIPf, TfSpawn.position, Quaternion.identity, listSportStore.transform);
                stationGUI.LoadStationInfoForBidding(child);
            }
        }
    }
    #endregion =========================================LoadDataStation=====================================

    #region =========================================Bidding=====================================

    public void LoadPopupBidding()
    {
        string data;
        dateBidding = System.DateTime.Now.ToString();
        data = dateBidding + "\n";
        data += "StationID: " + chosenStation.stationID + "\n";
        data += "Free Bidding: " + feeBiding.ToString();
        if (ClientData.Instance.ClientUser.isEnoughCoin(feeBiding))
        {
            buttonConfirmBidding.gameObject.SetActive(true);
            buttonToImportScene.gameObject.SetActive(false);
            textContentPopupBidding.color = Color.white;
        }
        else
        {
            buttonConfirmBidding.gameObject.SetActive(false);
            buttonToImportScene.gameObject.SetActive(true);
            data += "\n" + "Not enough coin to bidding";
            textContentPopupBidding.color = Color.red;
        }
        textContentPopupBidding.text = data;
    }

    public void ShowPopupBidding()
    {
        LoadPopupBidding();
        popupBidding.SetActive(true);
    }

    public async void BiddingStation()
    {
        ClientData.Instance.ClientUser.ChargeFee(feeBiding);
        coinControler.UpdateCoin();
        chosenStation.SetOwner(ClientData.Instance.ClientUser.userID, dateBidding);
        ClientData.Instance.ClientUser.clientStation.AddStation(chosenStation);
        ServerStation.UpdateOwner(chosenStation, dateBidding);
        chosenStationGUIControler.LoadStationInfoForBidding();
        Destroy(chosenStationGUIControler.gameObject, 4f);
        Invoke("ResizeStationContents",4.1f);
        popupBidding.SetActive(false);
        await FirebaseApi.Instance.PostUserValue("numCoin", ClientData.Instance.ClientUser.numCoin, PostDataCallback);
        await FirebaseApi.Instance.PostClientStation(chosenStation,PostDataCallback);
        await FirebaseApi.Instance.PostServerStation(chosenStation, PostDataCallback);
        popupBiddingSuccess.SetActive(true);
    }
    #endregion =========================================Bidding=====================================

    #region =========================================ChangeUIWithStationType=====================================

    public void ChangeUIElementsWithStationType(StationType _type)
    {
        if (ServerStation.NumStation(_type) > 0) noItemMessStation.SetActive(false);
        else noItemMessStation.SetActive(true);
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

    #region =========================================ChangeUIWithMarketState=====================================
    public void ChangeUIElementsWithMarketState(MarketState _state)
    {
        foreach (var child in UIElementsGroupByMarketState)
        {
            if (child.stationType == _state)
            {
                child.panel.SetActive(true);
                child.buttonChangeMarketState.image.color = new Color(0f, 1f, 1f);
                child.imageButton.color = Color.black;
            }
            else
            {
                child.panel.SetActive(false);
                child.buttonChangeMarketState.image.color = new Color(0.2f, 0.2f, 0.2f);
                child.imageButton.color = Color.white;
            }
        }
    }

    public void ChangUIToStationState()
    {
        ChangeUIElementsWithMarketState(MarketState.Station);
    }

    public void ChangUIToVehicleState()
    {
        ChangeUIElementsWithMarketState(MarketState.Vehicle);
    }

    #endregion =========================================ChangeUIWithMarketState=====================================

    #region =========================================ChangeUIWithVehicleType=====================================

    public void ChangeUIElementsWithVehicleType(NFTType _type)
    {
        noItemMessVehicle.SetActive(true);
        foreach (var child in UIElementsGroupByVehicleType)
        {
            if (child.VehicleType == _type)
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

    public void ChangeGUIToShoes()
    {
        ChangeUIElementsWithVehicleType(NFTType.Shoes);
    }

    public void ChangeGUIToBicycle()
    {
        ChangeUIElementsWithVehicleType(NFTType.Bicycle);
    }
    public void ChangeGUIToMotobike()
    {
        ChangeUIElementsWithVehicleType(NFTType.Motorbike);
    }
    public void ChangeGUIToCar()
    {
        ChangeUIElementsWithVehicleType(NFTType.Car);
    }

    #endregion =========================================ChangeUIWithVehicleType=====================================
    public void GotoImportScene()
    {
        ClientData.Instance.sceneBeforeImport = Scenes.MarketPlaceScene;
        SceneManager.LoadScene(Scenes.ImportScene.ToString(), LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(Scenes.MarketPlaceScene.ToString());
    }

    void PostDataCallback(string nameMethod, string message, int errorId)
    {
        Debug.Log("MarketPlaceSceneControler:" + nameMethod + message + ":" + errorId);
    }
}
