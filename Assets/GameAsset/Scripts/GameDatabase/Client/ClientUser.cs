using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class ClientUser
{
    public string email;
    public string userName;
    public string userID;
    public string address;
    public string userKey;
    public List<ClientCoin> clientCoins;
    public ClientNFT clientNFT;
    public ClientVehicle currentVehicle;

    public ClientUser(SpeedNDefault speedNDefault)
    {
        email = String.Empty;
        //userName = String.Empty;
        userName = "Minh ne";
        userID = String.Empty;
        // address = String.Empty;
        address = "0x4fd54DdF44C41B60F0Fa267Dda04B73Bf373768B";

        InitializeCoin(speedNDefault);
        
        clientNFT = new ClientNFT(speedNDefault);
        currentVehicle = clientNFT.clientVehicles[0];
    }

    public void InitializeCoin(SpeedNDefault speedNDefault){
         clientCoins = new List<ClientCoin>();
        int k = 1;
        for (int i = 0; i < speedNDefault.spriteIcons.Count; i++)
        {
            var clientCoin = new ClientCoin(speedNDefault.spriteIcons[i].name, k);
            clientCoins.Add(clientCoin);
            k += 10;
        }

    }

    public void CreateUserKey()
    {
        userKey = emailProcessed() + "-" + userID;
    }

    string emailProcessed()
    {
        string result = email;
        char[] specialChars = {'!', '#', '$','%' ,'&' ,'*','+' ,'-','/' ,'=','?', '^', '_',
        '`','{','|','}','"','(',')',',',':',';','<','>','@','[',']','.'};

        // foreach (char specialChar in specialChars)
        // {
        //     while (result.IndexOf(specialChar) != -1)
        //     {
        //         result = result.Remove(result.IndexOf(specialChar), 1);
        //     }
        // }

        int index = email.IndexOf('@');
        if (index > -1)
        {
            result = email.Remove(index);
        }

        return result;
    }

    public void SetUserKey(string _userKey)
    {
        userKey = _userKey;
    }

    public string GetStringJsonData()
    {
        return JsonUtility.ToJson(this);
    }

    public float GetAmountCoin(string nameCoin)
    {
        foreach (var child in clientCoins)
        {
            if (child.nameCoin == nameCoin) return child.amount;
        }
        return 0;
    }
}
