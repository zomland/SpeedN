using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class ServerStation
{
    public static List<Station> booster_stores= new List<Station>();
    public static List<Station> gas_stations= new List<Station>();
    public static List<Station> garages= new List<Station>();
    public static List<Station> sport_stores= new List<Station>();
    

}

[System.Serializable]
public class ClientStation
{
    public List<Station> Stations= new List<Station>();
    public ClientStation(){}

}

[System.Serializable]
public class Station
{
    public StationType stationType;
    public string ownerID="null";
    public string stationID="null";
    public float priceEnergy=0;
    public float priceRepair=0;
    public float taxPercent=0;

    public Station(){}
    public float FeeFillUp(float energyFillUp)
    {
        return energyFillUp*priceEnergy;
    }
    public float FeeRepair(float durabilityRepair)
    {
        return durabilityRepair*priceRepair;
    }

    public void SetPrice(float _price)
    {
        if(stationType==StationType.booster_store|stationType==StationType.gas_station)
            priceEnergy=_price;
        else priceRepair=_price;
    }
}

public enum StationType
{
    booster_store, gas_station, garage, sport_store
}
