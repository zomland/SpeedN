using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MotobikeAttribute : VehicleAttribute
{

    public MotobikeType Type;
    public MotobikeAttribute(string _name, string _id, VehicleRarity _rarity, MotobikeType _type
        , float _gasMax, float _gasPerMeter)
    {
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
        GasPerMeter=_gasPerMeter;
        GasMax = _gasMax;
        Gas = GasMax;
        Type = _type;
        LimitSpeed = new LimitsSpeed().Motobike[Type];
    }
}

public enum MotobikeType
{
    Urban, Suburban
}
