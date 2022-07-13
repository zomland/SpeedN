using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

[System.Serializable]
public class ClientMovingRecord
{
    public Dictionary<string, MovingRecord> movingRecords = new Dictionary<string, MovingRecord>();
    public ClientMovingRecord() { }

    public void LoadMovingRecords(Dictionary<string, MovingRecord> _movingRecords)
    {
        movingRecords = _movingRecords;
    }

    public void AddMovingRecordDetail(MovingRecord _record)
    {
        movingRecords.Add(_record.RecordID, _record);
    }

    public void DeleteExpiredRecord()
    {
        const int secondsPerMonth = 2592000;
        Debug.Log(System.DateTimeOffset.Now.ToUnixTimeSeconds());
        for (int index = 0; index < movingRecords.Count; index++)
        {
            MovingRecord _detail = movingRecords.ElementAt(index).Value;
            if (System.DateTimeOffset.Now.ToUnixTimeSeconds() - _detail.TimeStamp > secondsPerMonth
            & _detail.RecordID != "0000000")
            {
                movingRecords.Remove(movingRecords.ElementAt(index).Key);
                index--;
            }
        }
    }

    public string GetStringJsonData()
    {
        return JsonConvert.SerializeObject(this);
    }

    public int AmountRecord()
    {
        return movingRecords.Count;
    }
}

[System.Serializable]
public class MovingRecord
{
    public string Time = "null";
    public float NumCoin;
    public string VehicleName = "null";
    public string VehicleID = "null";
    public float Distance;
    public string TimeDroveString = "null";
    public string RecordID = "null";
    public long TimeStamp;

    public MovingRecord() { }
    public MovingRecord(string _time, float _numCoin, string _vehicleName, string _vehicleID, float _distance
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