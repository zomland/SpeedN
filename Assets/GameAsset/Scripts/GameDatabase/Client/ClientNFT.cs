using System;

[Serializable]
public abstract class NFTBaseStats
{
    public NFTType NftType;
}

public enum NFTType
{
    Car,
    Shoes,
    Bicycle,
    Motorbike
}
