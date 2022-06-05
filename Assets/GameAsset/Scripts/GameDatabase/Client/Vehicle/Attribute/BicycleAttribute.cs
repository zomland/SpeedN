using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BicycleAttribute : VehicleAttribute
{
    public BicycleType Type;
    public BicycleAttribute(string _name, string _id, VehicleRarity _rarity, BicycleType _type
        , float _staminaMax, float _staminaPerKm, float _coinPerKm)
    {
        vehicleType = VehicleType.Bicycle;
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
        Type = _type;
        LimitSpeed = new LimitsSpeed().Bicycle[Type];
        StaminaPesKm = _staminaPerKm;
        StaminaMax = _staminaMax;
        Stamina = StaminaMax;
        CoinPerKm=_coinPerKm;
    }
}

public enum BicycleType
{
    Beginner, Advance, Pro
}
