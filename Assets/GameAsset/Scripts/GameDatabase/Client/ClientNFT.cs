using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ClientNFT
{
    public List<ClientVehicle> clientVehicles;
    public List<ClientGem> clientGems;
    public List<ClientBlueprint> clientBlueprints;

    public ClientNFT(SpeedNDefault speedNDefault)
    {
        InitializeVehicles(speedNDefault);

        clientGems = new List<ClientGem>();
        clientGems.Add(null);   
        
        clientBlueprints = new List<ClientBlueprint>();
        clientBlueprints.Add(null);
    }

    public void InitializeVehicles(SpeedNDefault speedNDefault)
    {
        clientVehicles = new List<ClientVehicle>();
        for (int i = 0; i < speedNDefault.spriteVehicles.Count; i++)
        {
            ClientVehicle clientVehicle = new ClientVehicle(speedNDefault.spriteVehicles[i].name, i.ToString(), 1f, 1f, 12);
            clientVehicles.Add(clientVehicle);
        }
    }

}

[System.Serializable]
public class BaseNFT
{
    public int tokenID;
    public string ownerAddress;

    public BaseNFT()
    {
        tokenID = -1;
        ownerAddress = String.Empty;
    }
}
