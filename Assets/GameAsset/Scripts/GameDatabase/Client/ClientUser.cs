using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[System.Serializable]
public class ClientUser
{
    public string email = "null";
    public string userName = "null";
    public string userID = "null";
    public string address = "null";
    public string userKey = "null";
    public string currentVehicleID = "null";
    public float totalKm;
    public float totalTime;


    public ClientUser() { }

    public void CreateUserKey()
    {
        userKey = emailProcessed() + "-" + userID;
    }
    string emailProcessed()
    {
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

        // int index = email.IndexOf('@');
        // if (index > -1)
        // {
        //     result = email.Remove(index);
        // }

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

}
