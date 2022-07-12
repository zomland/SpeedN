using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class ModelVehicleBaseStats : NFTBaseStats
{
    public string ModelID;
    public string ModelName;
    public float Efficiency;
    public float EnergyMax;
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
    public static Dictionary<string, ModelVehicleBaseStats> StatsDict
        = new Dictionary<string, ModelVehicleBaseStats>();


    public static void AddModelStat(ModelVehicleBaseStats _modelStats)
    {
        StatsDict.Add(_modelStats.ModelID, _modelStats);
    }

}

