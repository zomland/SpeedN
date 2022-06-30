using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Base.Helper;

[System.Serializable]
public class ClientNFT
{

}

[System.Serializable]
public abstract class NFTBaseStats
{
    public NFTType NftType;
}

public enum NFTType
{
    [StringValue("Car")] Car,
    [StringValue("Sneakers")] Sneakers,
    [StringValue("Bicycle")] Bicycle,
    [StringValue("Motobike")] Motobike,
    [StringValue("MysteryBox")] MysteryBox
}

public enum NFTRarity 
{ 
    [StringValue("Common")] Common, 
    [StringValue("Uncommon")]Uncommon, 
    [StringValue("Rare")]Rare, 
    [StringValue("Legendary")]Legendary 
}
