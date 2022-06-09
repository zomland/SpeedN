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
    public string key = "0000000";
    public long timeStamp;

    public MovingRecordDetail() { }
    public MovingRecordDetail(string _time, float _numCoin, string _vehicleName, float _distance
        , string _timeDroveString, long _timeStamp)
    {
        time = _time;
        numCoin = _numCoin;
        vehicleName = _vehicleName;
        distance = _distance;
        timeDroveString = _timeDroveString;
        timeStamp = _timeStamp;
        key = timeStamp.ToString("0");
    }

    public string GetStringJsonData()
    {
        return JsonUtility.ToJson(this);
    }
}
