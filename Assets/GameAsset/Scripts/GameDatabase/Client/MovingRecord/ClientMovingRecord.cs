using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClientMovingRecord
{
    public float totalTime;
    public float totalKm;
    public List<MovingRecordDetail> movingRecordDetails = new List<MovingRecordDetail>();
    public ClientMovingRecord() { }

    public void AddMovingRecordDetail(MovingRecordDetail _detail)
    {
        movingRecordDetails.Add(_detail);
    }

    public string GetStringJsonData()
    {
        return JsonUtility.ToJson(this);
    }

    public int AmountRecord()
    {
        return movingRecordDetails.Count;
    }
}
