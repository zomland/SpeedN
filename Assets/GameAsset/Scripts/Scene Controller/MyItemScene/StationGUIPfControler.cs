using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationGUIPfControler : MonoBehaviour
{
    [HideInInspector]
    public Station station;
    public Text textStationInfo;
    public Image Background;

    Color[] ColorsStationType = new Color[]
    { new Color(0.3f, 0.5f, 0.5f),new Color(0.3f, 0.4f, 0.6f), new Color(0.7f, 0.5f, 0.3f),new Color(0.7f, 0.3f, 0.3f)};

    private void Start()
    {

    }

    public void LoadStationInfoForFillAndRepair(Station _station)
    {
        station = _station;
        string info;
        info = "StationID: " + station.stationID + "\n";
        info += "OwnerID: " + station.ownerID + "\n";
        info += "PricePerUnit: " + station.GetPrice().ToString("0.00");

        Background.color = ColorsStationType[(int)station.stationType];
        textStationInfo.text = info;
    }

    public void LoadStationInfoForBidding(Station _station)
    {
        station = _station;
        string info;
        info = "StationID: " + station.stationID + "\n";
        info += "OwnerID: " + station.ownerID + "\n";
        info += "FeeBidding: 300";
        Background.color = ColorsStationType[(int)station.stationType];
        textStationInfo.text = info;
    }

    public void ChooseStationOnItem()
    {
        PanelStationControler panelStationControler = FindObjectOfType<PanelStationControler>();
        panelStationControler.chosenStation = station;
        panelStationControler.ShowPopUpStation();
    }

    public void ChooseStationOnBidding()
    {
        BiddingSceneControler biddingSceneControler = FindObjectOfType<BiddingSceneControler>();
        biddingSceneControler.chosenStation = station;
        biddingSceneControler.chosenStationGUIPf = this.gameObject;
        biddingSceneControler.ShowPopupBidding();
    }

}
