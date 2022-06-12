using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ClientNFT
{
    public List<ClientVehicle> clientVehicles;
    // public List<ClientGem> clientGems;
    // public List<ClientBlueprint> clientBlueprints;

    public ClientNFT()
    {
        clientVehicles = new List<ClientVehicle>();

        // clientGems = new List<ClientGem>();
        // clientGems.Add(null);   
        //
        // clientBlueprints = new List<ClientBlueprint>();
        // clientBlueprints.Add(null);
    }


}

[System.Serializable]
public abstract class BaseNFT
{
    public int TokenId;
    public string OwnerAddress;
    public int Level;
    public NftRarity NftRarity;
    public float Stamina;
    public float Durability;
}

public enum NftRarity {Common, Uncommon, Rare, Legendary}
