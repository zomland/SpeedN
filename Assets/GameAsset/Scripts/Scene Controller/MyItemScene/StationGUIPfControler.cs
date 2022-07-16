using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationGUIPfControler : MonoBehaviour
{
    [HideInInspector]
    public Station station;

    [HideInInspector]
    public float price;

    public Text textStationInfo;
    PanelStationControler panelStationControler;


    private void Start()
    {
        panelStationControler = FindObjectOfType<PanelStationControler>();
    }

    public void LoadStationInfo(Station _station)
    {
        station = _station;
        string info;
        info = "StationID: " + station.stationID + "\n";
        info += "OwnerID: " + station.ownerID + "\n";
        if (station.stationType == StationType.booster_store || station.stationType == StationType.gas_station)
        {
            info += "PricePerUnit: " + station.priceEnergy.ToString("0.00") + "\n";
            price = station.priceEnergy;
        }
        else
        {
            info += "PricePerUnit: " + station.priceRepair.ToString("0.00") + "\n";
            price = station.priceRepair;
        }
        textStationInfo.text = info;
        Debug.Log(info);
    }

    public void ChooseStation()
    {
        panelStationControler.chosenStation = station;
        panelStationControler.ShowPopUpStation();
    }

}
