using System;

[Serializable]
public abstract class NFTBaseStats
{
    public NFTType NftType;
    public string ItemID;
    public string ModelID;
    public string NameItem;
}

public enum NFTType
{
    Car,
    Shoes,
    Bicycle,
    Motorbike
}
