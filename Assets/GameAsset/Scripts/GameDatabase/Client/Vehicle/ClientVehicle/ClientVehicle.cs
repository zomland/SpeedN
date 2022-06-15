using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientVehicle
{
    public VehicleAttribute Attrib;
    public ClientVehicle() { }
    public void LevelUp()
    {
        Attrib.Level++;
        Debug.Log("Level Up");
    }
    public void DecreaseDurability(float Km)
    {
        if (Attrib.Durability > 0f)
        {
            Attrib.Durability -= Attrib.DurabilityReducePerKm * Km;
            if (Attrib.Durability < 0) Attrib.Durability = 0f;
        }
        else
        {
            Debug.Log("Durability is zero");
        }
    }

    public float DurabilityPercent()
    {
        return Attrib.Durability / Attrib.DurabilityMax;
    }

    public void Repair()
    {
        Attrib.Durability = Attrib.DurabilityMax;
    }
    
    public virtual void UseEnergy(float Km) { }
    public virtual bool IsOutOfEnergy() { return true; }
    public virtual float EnergyPercent() { return 0f; }
    public virtual void RechargeEnergy() { }
    public virtual void FillUpEnergy() { }
}
