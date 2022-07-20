using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

[System.Serializable]
public static class ServerStation
{
    public static Dictionary<string, Station> booster_stores = new Dictionary<string, Station>();
    public static Dictionary<string, Station> gas_stations = new Dictionary<string, Station>();
    public static Dictionary<string, Station> garages = new Dictionary<string, Station>();
    public static Dictionary<string, Station> sport_stores = new Dictionary<string, Station>();

    public static void UpdatePrice(Station _station, string _dateUpdate)
    {
        switch (_station.stationType)
        {
            case StationType.booster_store:
                booster_stores[_station.stationID].SetPrice(_station.GetPrice(), _dateUpdate);
                break;
            case StationType.gas_station:
                gas_stations[_station.stationID].SetPrice(_station.GetPrice(), _dateUpdate);
                break;
            case StationType.garage:
                garages[_station.stationID].SetPrice(_station.GetPrice(), _dateUpdate);
                break;
            case StationType.sport_store:
                sport_stores[_station.stationID].SetPrice(_station.GetPrice(), _dateUpdate);
                break;
        }
    }
    public static void UpdateOwner(Station _station, string _dateUpdate)
    {
        switch (_station.stationType)
        {
            case StationType.booster_store:
                booster_stores[_station.stationID].SetOwner(_station.ownerID, _dateUpdate);
                break;
            case StationType.gas_station:
                gas_stations[_station.stationID].SetOwner(_station.ownerID, _dateUpdate);
                break;
            case StationType.garage:
                garages[_station.stationID].SetOwner(_station.ownerID, _dateUpdate);
                break;
            case StationType.sport_store:
                sport_stores[_station.stationID].SetOwner(_station.ownerID, _dateUpdate);
                break;
        }
    }
    public static int NumStation(StationType _type)
    {
        switch (_type)
        {
            case StationType.booster_store:
                return booster_stores.Count;
            case StationType.gas_station:
                return gas_stations.Count;
            case StationType.garage:
                return garages.Count;
            case StationType.sport_store:
                return sport_stores.Count;
        }
        return 0;
    }
    public static void UpdateNumCoin(Station _station)
    {
        switch (_station.stationType)
        {
            case StationType.booster_store:
                booster_stores[_station.stationID].numCoin = _station.numCoin;
                break;
            case StationType.gas_station:
                gas_stations[_station.stationID].numCoin = _station.numCoin;
                break;
            case StationType.garage:
                garages[_station.stationID].numCoin = _station.numCoin;
                break;
            case StationType.sport_store:
                sport_stores[_station.stationID].numCoin = _station.numCoin;
                break;
        }
    }
}

[System.Serializable]
public class ClientStation
{
    public Dictionary<string, Station> booster_stores = new Dictionary<string, Station>();
    public Dictionary<string, Station> gas_stations = new Dictionary<string, Station>();
    public Dictionary<string, Station> garages = new Dictionary<string, Station>();
    public Dictionary<string, Station> sport_stores = new Dictionary<string, Station>();

    public ClientStation() { }
    public void AddStation(Station _station)
    {
        string _stationID = _station.stationID;
        switch (_station.stationType)
        {
            case StationType.booster_store:
                booster_stores.Add(_stationID, ServerStation.booster_stores[_stationID]);
                break;
            case StationType.gas_station:
                gas_stations.Add(_stationID, ServerStation.gas_stations[_stationID]);
                break;
            case StationType.garage:
                garages.Add(_stationID, ServerStation.garages[_stationID]);
                break;
            case StationType.sport_store:
                sport_stores.Add(_stationID, ServerStation.sport_stores[_stationID]);
                break;
        }
    }

    public void UpdateNumCoin(Station _station)
    {
        switch (_station.stationType)
        {
            case StationType.booster_store:
                booster_stores[_station.stationID].numCoin = _station.numCoin;
                break;
            case StationType.gas_station:
                gas_stations[_station.stationID].numCoin = _station.numCoin;
                break;
            case StationType.garage:
                garages[_station.stationID].numCoin = _station.numCoin;
                break;
            case StationType.sport_store:
                sport_stores[_station.stationID].numCoin = _station.numCoin;
                break;
        }
    }

    public int NumStation(StationType _type)
    {
        switch (_type)
        {
            case StationType.booster_store:
                return booster_stores.Count;
            case StationType.gas_station:
                return gas_stations.Count;
            case StationType.garage:
                return garages.Count;
            case StationType.sport_store:
                return sport_stores.Count;
        }
        return 0;
    }
}

[System.Serializable]
public class Station
{
    public StationType stationType;
    public string ownerID;
    public string stationID;
    public string dateUpdate;
    public float priceEnergy = 0;
    public float priceRepair = 0;
    public float numCoin = 0;
    public float taxPercent = 0;

    public Station() { }

    public void SetOwner(string _ownerID, string _dateUpdate)
    {
        dateUpdate = _dateUpdate;
        ownerID = _ownerID;
    }
    public float FeeFillUp(float energyFillUp)
    {
        return energyFillUp * priceEnergy;
    }
    public float FeeRepair(float durabilityRepair)
    {
        return durabilityRepair * priceRepair;
    }

    public void SetPrice(float _price, string _dateUpdate)
    {
        dateUpdate = _dateUpdate;
        if (stationType == StationType.booster_store | stationType == StationType.gas_station)
            priceEnergy = _price;
        else priceRepair = _price;
    }

    public float GetPrice()
    {
        if (stationType == StationType.booster_store | stationType == StationType.gas_station)
            return priceEnergy;
        else return priceRepair;
    }

    public void ReceiveCoin(float amount)
    {
        numCoin += amount;
    }
    public void GetCoin(float amount)
    {
        if (amount <= numCoin) numCoin -= amount;
    }

    public string GetStringJsonData()
    {
        return JsonConvert.SerializeObject(this);
    }
}

[System.Serializable]
public enum StationType
{
    booster_store, gas_station, garage, sport_store
}
