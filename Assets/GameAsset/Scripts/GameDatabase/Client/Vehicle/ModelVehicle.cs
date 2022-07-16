using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class ModelVehicleBaseStats
{
    public NFTType NftType;
    public string ModelID;
    public string ModelName;
    public float Efficiency;
    public float Resiliency;
    public float EnergyMax;
    public float CoinPerMin;
    public float EnergyPerMinute;
    public float DurabilityMax;
    public float DurabilityPerMinute;


    public ModelVehicleBaseStats() { }
    public ModelVehicleBaseStats(string _ModelID)
    {

    }
    public ModelVehicleBaseStats FromJson(string DataJson)
    {
        return JsonConvert.DeserializeObject<ModelVehicleBaseStats>(DataJson);
    }
    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public static class ModelVehicle
{
    public static Dictionary<string, ModelVehicleBaseStats> ModelsDict
        = new Dictionary<string, ModelVehicleBaseStats>();


    public static void AddModelStat(ModelVehicleBaseStats _modelStats)
    {
        ModelsDict.Add(_modelStats.ModelID, _modelStats);
    }

}

