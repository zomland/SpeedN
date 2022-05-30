using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovingRecordManager
{
    [SerializeField]
    public List<MovingRecordDetail> movingRecordDetails;
    public MovingRecordManager(){
        movingRecordDetails= new List<MovingRecordDetail>();
    }

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
