using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VehicleAttribute
{
    public string Name;
    public string ID;
    public int Level;
    public float[] LimitSpeed;
    public float Efficiency;
    public VehicleType vehicleType;
    public VehicleRarity Rarity;
    public float Luck;
    public float Comfort;
    public float Resilience;
    public float Durability = 10;
    public int Mint;
    public int Lease;
    public float GasPerKm;
    public float GasMax;
    public float Gas;
    public float StaminaPesKm;
    public float Stamina;
    public float StaminaMax;
    public float CoinPerKm;
}

public enum VehicleRarity
{
    Common, Uncommon, Rare, Epic, Legendary
}

public enum VehicleType
{
    Car, Motobike, Shoes, Bicycle
}



