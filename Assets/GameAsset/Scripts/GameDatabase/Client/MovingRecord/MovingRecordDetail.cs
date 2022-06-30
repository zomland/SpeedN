using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class MovingRecordDetail
{
    public string Time = "null";
    public float NumCoin;
    public string VehicleName = "null";
    public string VehicleID = "null";
    public float Distance;
    public string TimeDroveString = "null";
    public string RecordID = "null";
    public long TimeStamp;

    public MovingRecordDetail() { }
    public MovingRecordDetail(string _time, float _numCoin, string _vehicleName, string _vehicleID, float _distance
        , string _timeDroveString, long _timeStamp)
    {
        Time = _time;
        NumCoin = _numCoin;
        VehicleName = _vehicleName;
        VehicleID = _vehicleID;
        Distance = _distance;
        TimeDroveString = _timeDroveString;
        TimeStamp = _timeStamp;
        RecordID = TimeStamp.ToString("0");
    }

    public string GetStringJsonData()
    {
        return JsonConvert.SerializeObject(this);
    }
}
