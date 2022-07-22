using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationGUIPfControler : MonoBehaviour
{
    [HideInInspector] public Station station;
    public Text textStationInfo;
    public Image Background;
    public Button buttonChoose;
    public Button buttonGetCoin;

    Color[] ColorsStationType = new Color[]
    { new Color(0.3f, 0.5f, 0.5f),new Color(0.3f, 0.4f, 0.6f), new Color(0.7f, 0.5f, 0.3f),new Color(0.7f, 0.3f, 0.3f)};

    private void Start()
    {

    }

    public void LoadStationInfoForFillAndRepair(Station _station)
    {
        station = _station;
        string info;
        info = "ID: " + station.stationID + "\n";
        if (_station.ownerID.Length == 0)
        {
            info += "No owner" + "\n";
        }
        else if (_station.ownerID == ClientData.Instance.ClientUser.userID) info += "Your Station" + "\n";
        else info += "OwnerID : " + station.ownerID + "\n";
        info += "Price : " + station.GetPrice().ToString("0");
        Background.color = ColorsStationType[(int)station.stationType];
        textStationInfo.text = info;
    }

    public void LoadStationInfoForBidding(Station _station)
    {
        station = _station;
        string info;
        info = "StationID: " + station.stationID + "\n";
        if (_station.ownerID.Length == 0)
        {
            info += "No owner" + "\n";
        }
        else if (_station.ownerID == ClientData.Instance.ClientUser.userID) info += "Your Station" + "\n";
        else info += "OwnerID: " + station.ownerID + "\n";
        info += "FeeBidding: 300";
        Background.color = ColorsStationType[(int)station.stationType];
        textStationInfo.text = info;
    }

    public void LoadStationInfoForBidding()
    {
        string info;
        info = "StationID: " + station.stationID + "\n";
        if (station.ownerID.Length == 0)
        {
            info += "No owner" + "\n";
        }
        else if (station.ownerID == ClientData.Instance.ClientUser.userID)
        {
            info += "Your Station" + "\n";
            buttonChoose.interactable = false;
        }
        else info += "OwnerID: " + station.ownerID + "\n";
        info += "FeeBidding: 300";
        Background.color = ColorsStationType[(int)station.stationType];
        textStationInfo.text = info;
    }

    public void LoadStationInfoForMyStation(Station _station)
    {
        station = _station;
        string info;
        info = "StationID: " + station.stationID + "\n";
        info += "Price: " + station.GetPrice().ToString("0.0") + "\n";
        info += "Coin: " + station.numCoin.ToString();
        Background.color = ColorsStationType[(int)station.stationType];
        textStationInfo.text = info;
        if (_station.numCoin == 0) buttonGetCoin.interactable = false;
        else buttonGetCoin.interactable = true;
    }
    public void LoadStationInfoForMyStation()
    {
        string info;
        info = "StationID: " + station.stationID + "\n";
        info += "Update" + "\n";
        info += station.dateUpdate + "\n";
        info += "Price: " + station.GetPrice().ToString("0.0") + "\n";
        info += "Coin: " + station.numCoin.ToString();
        Background.color = ColorsStationType[(int)station.stationType];
        textStationInfo.text = info;
        if (station.numCoin == 0) buttonGetCoin.interactable = false;
        else buttonGetCoin.interactable = true;
    }

    public void ChooseStationOnItem()
    {
        PanelStationOnItemControler panelStationOnItemControler = FindObjectOfType<PanelStationOnItemControler>();
        panelStationOnItemControler.chosenStation = station;
        panelStationOnItemControler.ShowPopUpStation();
    }

    public void ChooseStationOnBidding()
    {
        MarketPlaceSceneControler marketPlaceSceneControler = FindObjectOfType<MarketPlaceSceneControler>();
        marketPlaceSceneControler.chosenStation = station;
        marketPlaceSceneControler.chosenStationGUIControler = this;
        marketPlaceSceneControler.ShowPopupBidding();
    }

    public void ChooseStationToSetPrice()
    {
        MyStationSceneControler myStationSceneControler = FindObjectOfType<MyStationSceneControler>();
        myStationSceneControler.chosenStation = station;
        myStationSceneControler.chosenStationGUIControler = this;
        myStationSceneControler.ShowPopupSetPriceStation();
    }

    public void ChooseStationToGetCoin()
    {
        MyStationSceneControler myStationSceneControler = FindObjectOfType<MyStationSceneControler>();
        myStationSceneControler.chosenStation = station;
        myStationSceneControler.chosenStationGUIControler = this;
        myStationSceneControler.ShowPopupGetCoinFromStation();
    }

}
