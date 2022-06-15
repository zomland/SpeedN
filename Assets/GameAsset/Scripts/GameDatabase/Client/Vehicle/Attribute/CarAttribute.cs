using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CarAttribute : VehicleAttribute
{
    public CarType Type;
    public CarAttribute(string _name, string _id, VehicleRarity _rarity, CarType _type
        , float _gasMax, float _gasPerKm, float _coinPerKm)
    {
        vehicleType = VehicleType.Car;
        Name = _name;
        ID = _id;
        Level = 0;
        Efficiency = 1f;
        Rarity = _rarity;
        Luck = 0f;
        Comfort = 0f;
        Resilience = 0f;
        Durability = 1f;
        Mint = 0;
        Lease = 0;
        GasPerKm = _gasPerKm;
        GasMax = _gasMax;
        Gas = GasMax;
        Type = _type;
        DurabilityMax = 500f;
        Durability = DurabilityMax;
        LimitSpeed = new LimitsSpeed().Car[Type];
        CoinPerKm = _coinPerKm;
    }
}

public enum CarType
{
    Urban, Suburban
}
