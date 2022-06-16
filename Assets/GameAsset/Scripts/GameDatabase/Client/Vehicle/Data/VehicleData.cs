using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VehicleData : BaseNFT
{
    public float[] LimitSpeed;
    public float Efficiency;
    public VehicleType vehicleType;
    public float Luck;
    public float Comfort;
    public float Resilience;
    public float DurabilityMax;
    public float Durability;
    public float DurabilityReducePerKm;
    public int Mint;
    public int Lease;
    public float Energy;
    public float EnergyMax;
    public float EnergyPerKm;
    public float CoinPerKm;

    public VehicleData() { }

    public VehicleData(string _name, string _ID, string _ownerAddress, NftRarity _NftRarity, float[] _limitSpeed, VehicleType _vehicleType, float _durabilityMax
        , float _durabilityReducePerKm, float _energyMax, float _energyPerKm, float _coinPerKm)
    {
        name = _name;
        TokenId = _ID;
        OwnerAddress = _ownerAddress;
        Level = 0;
        NftRarity = _NftRarity;
        LimitSpeed = _limitSpeed;
        Efficiency = 1f;
        vehicleType = _vehicleType;
        Luck = 1f;
        Comfort = 1f;
        Resilience = 1f;
        DurabilityMax = _durabilityMax;
        Durability = DurabilityMax;
        DurabilityReducePerKm = _durabilityReducePerKm;
        Mint = 1;
        Lease = 1;
        EnergyMax = _energyMax;
        Energy = EnergyMax;
        EnergyPerKm = _energyPerKm;
        CoinPerKm = _coinPerKm;
    }
}

public enum VehicleType
{
    Car, Motobike, Shoes, Bicycle
}



