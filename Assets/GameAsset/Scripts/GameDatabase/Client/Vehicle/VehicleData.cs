using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class VehicleData
{
    public string ModelID;
    public string ItemID;
    public string NameItem;
    public float Durability;
    public float Energy;
    public VehicleData()
    {
        ModelID = "null";
        ItemID = "null";
        NameItem = "null";
    }
    public VehicleData(string _modelID, string _itemID)
    {
        ModelID = _modelID;
        ItemID = _itemID;
    }

    public string GetStringJsonData()
    {
        return JsonConvert.SerializeObject(this);
    }
}




