using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ClientNFT
{
    public List<VehicleController> vehicleControllers;
    // public List<ClientGem> clientGems;
    // public List<ClientBlueprint> clientBlueprints;

    public ClientNFT()
    {
        vehicleControllers = new List<VehicleController>();

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
    public string name;
    public string TokenId;
    public string OwnerAddress;
    public int Level;
    public NftRarity NftRarity;
}

public enum NftRarity { Common, Uncommon, Rare, Epic, Legendary }
