using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VehicleController
{
    public VehicleData data;
    public VehicleController(VehicleData _data) { data = _data; }
    public void LevelUp()
    {
        data.Level++;
        Debug.Log("Level Up");
    }
    public void DecreaseDurability(float Km)
    {
        if (data.Durability > 0f)
        {
            data.Durability -= data.DurabilityReducePerKm * Km;
            if (data.Durability < 0) data.Durability = 0f;
        }
        else
        {
            Debug.Log("Durability is zero");
        }
    }

    public float DurabilityPercent()
    {
        return data.Durability / data.DurabilityMax;
    }
    public void Repair()
    {
        data.Durability = data.DurabilityMax;
    }
    public void UseEnergy(float Km)
    {
        if (data.Energy > 0f)
        {
            data.Energy -= data.EnergyPerKm * Km;
            if (data.Energy < 0) data.Energy = 0f;
        }
        else
        {
            Debug.Log("Out of energy");
        }
    }
    public bool IsOutOfEnergy()
    {
        return data.Energy <= 0f;
    }
    public float EnergyPercent()
    {
        return data.Energy / data.EnergyMax;
    }
    public void FillUpEnergy()
    {
        data.Energy = data.EnergyMax;
    }
}
