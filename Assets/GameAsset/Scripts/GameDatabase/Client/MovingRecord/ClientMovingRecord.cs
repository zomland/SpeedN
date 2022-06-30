using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

[System.Serializable]
public class ClientMovingRecord
{
    public Dictionary<string, MovingRecordDetail> movingRecordDetails = new Dictionary<string, MovingRecordDetail>();
    public ClientMovingRecord() { }

    public void LoadMovingRecords(Dictionary<string, MovingRecordDetail> _movingRecordDetails)
    {
        movingRecordDetails = _movingRecordDetails;
    }

    public void AddMovingRecordDetail(MovingRecordDetail _detail)
    {
        movingRecordDetails.Add(_detail.RecordID, _detail);
    }

    public void DeleteExpiredRecord()
    {
        const int secondsPerMonth = 2592000;
        Debug.Log(System.DateTimeOffset.Now.ToUnixTimeSeconds());
        for (int index = 0; index < movingRecordDetails.Count; index++)
        {
            MovingRecordDetail _detail = movingRecordDetails.ElementAt(index).Value;
            if (System.DateTimeOffset.Now.ToUnixTimeSeconds() - _detail.TimeStamp > secondsPerMonth
            & _detail.RecordID != "0000000")
            {
                movingRecordDetails.Remove(movingRecordDetails.ElementAt(index).Key);
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
        return movingRecordDetails.Count;
    }
}
