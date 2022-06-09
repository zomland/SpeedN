using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FirebaseHandler;
using System.Linq;
using System;

public class MovingRecordDetailControler : MonoBehaviour
{
    public Text textMessageMovingRecord;
    public RawImage currentVehicleRawImg;
    public Text textUserName;
    public Text textTime;
    public Text textNumCoinRecord;
    public Text textDistanceRecord;
    public Text textUnitDistance;
    public Text textTimeDrove;
    public RawImage userAvatar;

    MovingRecordDetail _movingRecordDetail;

    public void CreateMovingRecord(float _numCoin, string _vehicleName, float _distance
        , string _timeDroveString, float _timeDrove)
    {
        string _timeCreate = System.DateTime.Now.ToString();
        long _timeStamp = System.DateTimeOffset.Now.ToUnixTimeSeconds();
        _movingRecordDetail = new MovingRecordDetail(_timeCreate, _numCoin, _vehicleName, _distance, _timeDroveString, _timeStamp);
        ClientData.Instance.clientMovingRecord.totalKm += _distance;
        ClientData.Instance.clientMovingRecord.totalTime += _timeDrove;
        ClientData.Instance.clientMovingRecord.AddMovingRecordDetail(_movingRecordDetail);
        FirebaseApi.Instance.AddAMovingRecord(_movingRecordDetail, OnAddAMovingRecord).Forget();
    }

    public void LoadDataMovingRecord(int index)
    {
        _movingRecordDetail
            = ClientData.Instance.clientMovingRecord.movingRecordDetails.ElementAt(index).Value;
    }
    public void DisplayMovingRecord()
    {
        textMessageMovingRecord.text = "Using vehicle: " + _movingRecordDetail.vehicleName;
        currentVehicleRawImg.texture
            = ClientData.Instance.GetSpriteVehicle(_movingRecordDetail.vehicleName).sprite.texture;
        textUserName.text = ClientData.Instance.ClientUser.userName;
        textTime.text = _movingRecordDetail.time;
        ShowDistanceAndNumCoin();
        textTimeDrove.text = _movingRecordDetail.timeDroveString;
        //userAvatar;
        this.gameObject.SetActive(true);
    }

    void ShowDistanceAndNumCoin()
    {
        if (_movingRecordDetail.distance < 1)
        {
            textUnitDistance.text = "m";
            textDistanceRecord.text = (_movingRecordDetail.distance * 1000).ToString("0.0");
        }
        else
        {
            textUnitDistance.text = "Km";
            textDistanceRecord.text = (_movingRecordDetail.distance).ToString("0.0");
        }
        if (_movingRecordDetail.numCoin < 0.01f & _movingRecordDetail.numCoin > 0f)
        {
            textNumCoinRecord.text = _movingRecordDetail.numCoin.ToString("0.000");
        }
        else
        {
            textNumCoinRecord.text = _movingRecordDetail.numCoin.ToString("0.00");
        }
    }

    public void PostRecords()
    {
        Debug.LogWarning("Save Record");
    }

    void OnAddAMovingRecord(string nameProcedure, string message, int Id)
    {
        Debug.Log("OnAddAMovingRecord");
    }

}
