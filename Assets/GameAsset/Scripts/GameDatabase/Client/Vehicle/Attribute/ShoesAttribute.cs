using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShoesAttribute : VehicleAttribute
{
    public ShoesType Type;
    public ShoesAttribute(string _name, string _id, VehicleRarity _rarity, ShoesType _type
        , float _staminaMax, float _staminaPerMeter)
    {
        vehicleType = VehicleType.Shoes;
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
        LimitSpeed = new LimitsSpeed().Shoes[Type];
        StaminaPesMeter = _staminaPerMeter;
        StaminaMax = _staminaMax;
        Stamina = StaminaMax;
    }
}

public enum ShoesType
{
    Walker, Runner, Trainer
}
