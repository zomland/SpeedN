using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FirebaseHandler;
using TMPro;

public class BiddingSceneControler : MonoBehaviour
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
    [Header("PopUp")]
    public GameObject popupBidding;
    public GameObject popupBiddingSuccess;
    public Text textContentPopupBidding;
    public Button buttonConfirmBidding;
    [Header("Prefabs")]
    public StationGUIPfControler stationGUIPf;
    public AmountCoin coinControler;

    [HideInInspector] public GameObject chosenStationGUIPf;
    [HideInInspector] public Station chosenStation;

    float feeBiding = 300;
    // Start is called before the first frame update
    void Start()
    {
        LoadListsStation();
        ChangeBiddingUIElements(StationType.booster_store);
    }

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

    public void LoadPopupBidding()
    {
        string data;
        data = System.DateTime.Now.ToString() + "\n";
        data += "StationID: " + chosenStation.stationID + "\n";
        data += "Free Bidding: " + feeBiding.ToString();
        if (ClientData.Instance.ClientUser.isEnoughCoin(feeBiding))
        {
            buttonConfirmBidding.interactable = true;
            textContentPopupBidding.color = Color.white;
        }
        else
        {
            buttonConfirmBidding.interactable = false;
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
        chosenStation.SetOwner(ClientData.Instance.ClientUser.userID);
        ClientData.Instance.ClientUser.clientStation.AddStation(chosenStation);
        Destroy(chosenStationGUIPf, 1f);
        popupBidding.SetActive(false);
        await FirebaseApi.Instance.PostUserValue("numCoin", ClientData.Instance.ClientUser.numCoin, PostDataCallback);
        await FirebaseApi.Instance.PostClientStation(PostDataCallback);
        await FirebaseApi.Instance.PostServerStationOwner(chosenStation, PostDataCallback);
        popupBiddingSuccess.SetActive(true);
    }


    public void ChangeBiddingUIElements(StationType _type)
    {
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
        ChangeBiddingUIElements(StationType.booster_store);
    }

    public void ChangeGUIToGasStation()
    {
        ChangeBiddingUIElements(StationType.gas_station);
    }
    public void ChangeGUIToGarage()
    {
        ChangeBiddingUIElements(StationType.garage);
    }
    public void ChangeGUIToSportStore()
    {
        ChangeBiddingUIElements(StationType.sport_store);
    }

    void PostDataCallback(string nameMethod, string message, int errorId)
    {
        Debug.Log("BiddingSceneControler:" + nameMethod + message + ":" + errorId);
    }
}
