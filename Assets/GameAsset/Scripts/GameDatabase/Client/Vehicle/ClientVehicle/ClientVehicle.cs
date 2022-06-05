using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientVehicle
{
    public VehicleAttribute Attrib;
    public ClientVehicle() { }
    public virtual void LevelUp()
    {
        Attrib.Level++;
        Debug.Log("Level Up");
    }
    public virtual void DecreaseDurability(float reducedDurability)
    {
        if(Attrib.Durability>0)
        {
            Attrib.Durability -= reducedDurability;
        }
    }
    public virtual void Repair()
    {
        Attrib.Durability = 10f;
    }
    public virtual void UseEnergy(float Km) { }
    public virtual bool IsOutOfEnergy() { return true; }
    public virtual float EnergyPercent() { return 0f; }
    public virtual void FillUpEnergy() { }
}
