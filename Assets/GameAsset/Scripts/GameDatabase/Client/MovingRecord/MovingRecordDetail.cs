using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovingRecordDetail
{
    public string time;
    public float numCoin;
    public string vehicleName;
    public string vehicleID;
    public float distance;
    public string timeDroveString;

    public MovingRecordDetail() { }
    public MovingRecordDetail(string _time, float _numCoin, string _vehicleName, float _distance
        , string _timeDroveString)
    {
        time = _time;
        numCoin = _numCoin;
        vehicleName = _vehicleName;
        distance = _distance;
        timeDroveString = _timeDroveString;
    }

    public string GetStringJsonData()
    {
        return JsonUtility.ToJson(this);
    }
}
