using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class ClientVehicle
{
    public Vehicle currentVehicle = new Vehicle();
    public List<Vehicle> Vehicles = new List<Vehicle>();
    public ClientVehicle() { }

    public void CreateFromLocal(SpeedNDefault speedNDefault)
    {
        foreach (var child in speedNDefault.spriteModelVehicles)
        {
            string _itemID = UnityEngine.Random.Range(1000, 9999).ToString("0000");
            Vehicles.Add(new Vehicle(child.spriteID, _itemID));
        }
        currentVehicle =  Vehicles[0];
    }

    public void UpLoadCurrentVehicle()
    {
        foreach (var child in Vehicles)
        {
            if (child.ItemID == currentVehicle.ItemID) currentVehicle = child;
        }
    }

    public string GetModelID(string _itemID)
    {
        foreach (var child in Vehicles)
            if (child.ItemID == _itemID) return child.ModelID;
        return null;
    }
}

[System.Serializable]
public class Vehicle : NFTBaseStats
{
    public float Durability;
    public float Energy;

    public Vehicle() { }

    public Vehicle(string _modelID, string _itemID)
    {
        ModelID = _modelID;
        ItemID = _itemID;
        NameItem = ModelStats().ModelName;
        InitialLoad();
    }

    public void InitialLoad()
    {
        ModelVehicleBaseStats modelStat = ModelStats();
        Energy = modelStat.EnergyMax;
        Durability = modelStat.DurabilityMax;
        NameItem = modelStat.ModelName;
    }

    public ModelVehicleBaseStats ModelStats()
    {
        return ModelVehicle.ModelsDict[ModelID];
    }

    public void DecreaseDurability(float _minute)
    {
        if (Durability > 0f)
        {
            Durability -= ModelStats().DurabilityPerMinute * _minute;
            if (Durability < 0) Durability = 0f;
        }
        else
        {
            Debug.Log("Durability is zero");
        }
    }

    public float DurabilityPercent()
    {
        return Durability / ModelStats().DurabilityMax;
    }
    public void Repair(float _amount)
    {
        Durability += _amount;
        if (Durability >= ModelStats().DurabilityMax) Durability = ModelStats().DurabilityMax;
    }
    public void UseEnergy(float _minute)
    {
        if (Energy > 0f)
        {
            Energy -= ModelStats().EnergyPerMinute * _minute;
            if (Energy < 0) Energy = 0f;
        }
        else
        {
            Debug.Log("Out of energy");
        }
    }
    public bool IsOutOfEnergy()
    {
        return Energy <= 0f;
    }
    public float EnergyPercent()
    {
        return Energy / ModelStats().EnergyMax;
    }
    public void FillUpEnergy(float _amount)
    {
        Energy += _amount;
        if (Energy >= ModelStats().EnergyMax) Energy = ModelStats().EnergyMax;
    }

    public float LostEnergy()
    {
        return ModelStats().EnergyMax - Energy;
    }

    public float LostDurability()
    {
        return ModelStats().DurabilityMax - Durability;
    }

    public string EnergyName()
    {
        if (NftType == NFTType.Car || NftType == NFTType.Motorbike) return "Gas";
        else return "Stamina";
    }
    public string GetStringJsonData()
    {
        return JsonConvert.SerializeObject(this);
    }
}
