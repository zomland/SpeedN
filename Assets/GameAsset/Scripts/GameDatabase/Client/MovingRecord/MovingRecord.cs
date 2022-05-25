using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRecord
{
    public string time;
    public float numCoin;
    public string vehicleName;
    public string vehicleID;
    public float distance;
    public string timeDroveString;

    public MovingRecord(){}
    public MovingRecord(string _time, float _numCoin, string _vehicleName, float _distance
        , string _timeDroveString)
    {
        time=_time;
        numCoin=_numCoin;
        vehicleName=_vehicleName;
        distance=_distance;
        timeDroveString=_timeDroveString;
    }

}
