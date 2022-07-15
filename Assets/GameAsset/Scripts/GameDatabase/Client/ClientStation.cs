using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public static class ServerStation
{
    public static Dictionary<string, Station> booster_stores = new Dictionary<string, Station>();
    public static Dictionary<string, Station> gas_stations = new Dictionary<string, Station>();
    public static Dictionary<string, Station> garages = new Dictionary<string, Station>();
    public static Dictionary<string, Station> sport_stores = new Dictionary<string, Station>();
}

[System.Serializable]
public class ClientStation
{
    public Dictionary<string, Station> Stations = new Dictionary<string, Station>();
    public ClientStation() { }
    public void AddStation(Station _station)
    {
        Stations.Add(_station.stationID, _station);
    }
    public void AddStation(StationType _type, string _stationID)
    {
        switch (_type)
        {
            case StationType.booster_store:
                Stations.Add(_stationID, ServerStation.booster_stores[_stationID]);
                break;
            case StationType.gas_station:
                Stations.Add(_stationID, ServerStation.gas_stations[_stationID]);
                break;
            case StationType.garage:
                Stations.Add(_stationID, ServerStation.garages[_stationID]);
                break;
            case StationType.sport_store:
                Stations.Add(_stationID, ServerStation.sport_stores[_stationID]);
                break;
        }
    }

}

[System.Serializable]
public class Station
{
    public StationType stationType;
    public string ownerID;
    public string stationID;
    public float priceEnergy = 0;
    public float priceRepair = 0;
    public float taxPercent = 0;

    public Station() { }
    public float FeeFillUp(float energyFillUp)
    {
        return energyFillUp * priceEnergy;
    }
    public float FeeRepair(float durabilityRepair)
    {
        return durabilityRepair * priceRepair;
    }

    public void SetPrice(float _price)
    {
        if (stationType == StationType.booster_store | stationType == StationType.gas_station)
            priceEnergy = _price;
        else priceRepair = _price;
    }
}

public enum StationType
{
    booster_store, gas_station, garage, sport_store
}
