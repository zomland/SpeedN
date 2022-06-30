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

    public void CreateMovingRecord(float _numCoin, string _vehicleName, string _vehicleID, float _distance
        , string _timeDroveString, float _timeDrove)
    {
        string _timeCreate = System.DateTime.Now.ToString();
        long _timeStamp = System.DateTimeOffset.Now.ToUnixTimeSeconds();
        _movingRecordDetail = new MovingRecordDetail(_timeCreate, _numCoin, _vehicleName, _vehicleID, _distance, _timeDroveString, _timeStamp);
        ClientData.Instance.ClientUser.totalKm += _distance;
        ClientData.Instance.ClientUser.totalTime += _timeDrove;
        ClientData.Instance.ClientMovingRecord.AddMovingRecordDetail(_movingRecordDetail);
        DatabaseHandler.SaveAMovingRecord(_movingRecordDetail, SaveMovingRecordCallback);
    }

    public void LoadDataMovingRecord(int index)
    {
        _movingRecordDetail
            = ClientData.Instance.ClientMovingRecord.movingRecordDetails.ElementAt(index).Value;
    }
    public void DisplayMovingRecord()
    {
        textMessageMovingRecord.text = "Using vehicle: " + _movingRecordDetail.VehicleName;

        string ModelVehicleID = ClientData.Instance.ClientVehicle.GetModelID(_movingRecordDetail.VehicleID);
        Debug.LogWarning(ModelVehicleID + "+" + _movingRecordDetail.VehicleID);
        currentVehicleRawImg.texture
            = ClientData.Instance.GetSpriteModelVehicle(ModelVehicleID).sprite.texture;
        textUserName.text = ClientData.Instance.ClientUser.userName;
        textTime.text = _movingRecordDetail.Time;
        ShowDistanceAndNumCoin();
        textTimeDrove.text = _movingRecordDetail.TimeDroveString;
        //userAvatar;
        this.gameObject.SetActive(true);
    }

    void ShowDistanceAndNumCoin()
    {
        if (_movingRecordDetail.Distance < 1)
        {
            textUnitDistance.text = "m";
            textDistanceRecord.text = (_movingRecordDetail.Distance * 1000).ToString("0.0");
        }
        else
        {
            textUnitDistance.text = "Km";
            textDistanceRecord.text = (_movingRecordDetail.Distance).ToString("0.0");
        }
        if (_movingRecordDetail.NumCoin < 0.01f & _movingRecordDetail.NumCoin > 0f)
        {
            textNumCoinRecord.text = _movingRecordDetail.NumCoin.ToString("0.000");
        }
        else
        {
            textNumCoinRecord.text = _movingRecordDetail.NumCoin.ToString("0.00");
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

    public static void SaveMovingRecordCallback(string message)
    {
        Debug.Log("SaveMovingRecordCallbackOnRecordControler: " + message);
    }



}
