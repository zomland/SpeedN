using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[System.Serializable]
public class ClientUser
{
    public string email;
    public string userName;
    public string userID;
    public string address;
    public string userKey;
    public float totalKm;
    public float totalTime;
    public float numCoin;

    public ClientVehicle clientVehicle = new ClientVehicle();
    public ClientMovingRecord clientMovingRecord = new ClientMovingRecord();
    public ClientStation clientStation = new ClientStation();


    public ClientUser() { }

    public void CreateUserKey()
    {
        userKey = emailProcessed() + "_" + userID;
    }
    string emailProcessed()
    {
        if (email == null | email == "null") return null;
        string result = email;
        char[] specialChars = {'!', '#', '$','%' ,'&' ,'*','+' ,'-','/' ,'=','?', '^', '_',
        '`','{','|','}','"','(',')',',',':',';','<','>','@','[',']','.'};

        foreach (char specialChar in specialChars)
        {
            while (result.IndexOf(specialChar) != -1)
            {
                result = result.Remove(result.IndexOf(specialChar), 1);
            }
        }
        return result;
    }

    public void SetUserKey(string _userKey)
    {
        userKey = _userKey;
    }

    public string GetStringJsonData()
    {
        return JsonConvert.SerializeObject(this);
    }


    public bool isEnoughCoin(float fee)
    {
        return numCoin >= fee;
    }

    public void ChargeFee(float fee)
    {
        if (isEnoughCoin(fee)) numCoin -= fee;
    }

    public void ReceiveCoinFromDriving(float amount)
    {
        numCoin += amount;
    }

    public void ReceiveCoinFromStation(float amount, float taxPercent)
    {
        numCoin += amount * (1 - taxPercent);
    }



}
