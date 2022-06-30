using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientVehicle
{
    public Vehicle currentVehicle;
    public List<Vehicle> Vehicles = new List<Vehicle>();
    public ClientVehicle() { }

    public void CreateFromLocal(SpeedNDefault speedNDefault)
    {
        foreach (var child in speedNDefault.spriteModelVehicles)
        {
            string _itemID = UnityEngine.Random.Range(1000, 9999).ToString("0000");
            VehicleData vehicleData = new VehicleData(child.spriteID, _itemID);
            Vehicles.Add(new Vehicle(vehicleData));
        }
    }

    public void InitialLoad(string _currentVehicleID)
    {
        if (_currentVehicleID != "null")
        {
            foreach (var vehicle in Vehicles)
            {
                if (vehicle.Data.ItemID == _currentVehicleID) currentVehicle = vehicle;
            }
        }
        else
        {
            if (Vehicles.Count > 0)
            {
                currentVehicle = Vehicles[0];
            }
        }
    }
    public string GetModelID(string _itemID)
    {
        foreach (var child in Vehicles)
            if (child.Data.ItemID == _itemID) return child.BaseStats.ModelID;
        return null;
    }

}

[System.Serializable]
public class Vehicle
{

    public ModelVehicleBaseStats BaseStats;
    public VehicleData Data;

    public Vehicle(VehicleData _data)
    {
        BaseStats = ModelVehicle.StatsDict[_data.ModelID];
        Data = _data;
        Data.NameItem = BaseStats.ModelName;
        InitialLoad();
    }

    public void InitialLoad()
    {
        Data.Energy = BaseStats.EnergyMax;
        Data.Durability = BaseStats.DurabilityMax;
        Data.NameItem = BaseStats.ModelName;
    }

    public void DecreaseDurability(float _minute)
    {
        if (Data.Durability > 0f)
        {
            Data.Durability -= BaseStats.DurabilityPerMinute * _minute;
            if (Data.Durability < 0) Data.Durability = 0f;
        }
        else
        {
            Debug.Log("Durability is zero");
        }
    }

    public float DurabilityPercent()
    {
        return Data.Durability / BaseStats.DurabilityMax;
    }
    public void Repair()
    {
        Data.Durability = BaseStats.DurabilityMax;
    }
    public void UseEnergy(float _minute)
    {
        if (Data.Energy > 0f)
        {
            Data.Energy -= BaseStats.EnergyPerMinute * _minute;
            if (Data.Energy < 0) Data.Energy = 0f;
        }
        else
        {
            Debug.Log("Out of energy");
        }
    }
    public bool IsOutOfEnergy()
    {
        return Data.Energy <= 0f;
    }
    public float EnergyPercent()
    {
        return Data.Energy / BaseStats.EnergyMax;
    }
    public void FillUpEnergy()
    {
        Data.Energy = BaseStats.EnergyMax;
    }
}
