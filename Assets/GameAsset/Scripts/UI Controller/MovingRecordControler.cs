using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FirebaseHandler;

public class MovingRecordControler : MonoBehaviour
{
    public GameObject MovingRecordDetailScene;
    public Text textMessageMovingRecord;
    public RawImage currentVehicleRawImg;
    public Text textUserName;
    public Text textTime;
    public Text textNumCoinRecord;
    public Text textDistanceRecord;
    public Text textTimeDrove;
    public RawImage userAvatar;

    MovingRecordDetail _movingRecordDetail;

    public void CreateMovingRecord(float _numCoin, string _vehicleName, float _distance
        , string _timeDroveString)
    {
        string timeCreate = System.DateTime.Now.ToString();
        _movingRecordDetail = new MovingRecordDetail(timeCreate, _numCoin, _vehicleName, _distance, _timeDroveString);
        ClientData.Instance._movingRecordManager.AddMovingRecordDetail(_movingRecordDetail);
        FirebaseApi.Instance.AddAMovingRecord(_movingRecordDetail,OnAddAMovingRecord).Forget();
        
        
        
    }
    public void DisplayMovingRecord()
    {
        textMessageMovingRecord.text = "Using vehicle: " + _movingRecordDetail.vehicleName;
        currentVehicleRawImg.texture
            = ClientData.Instance.GetSpriteVehicle(_movingRecordDetail.vehicleName).sprite.texture;
        textUserName.text = ClientData.Instance.ClientUser.userName;
        textTime.text = _movingRecordDetail.time;
        textNumCoinRecord.text = _movingRecordDetail.numCoin.ToString("0.0");
        ShowDistance();
        textTimeDrove.text = _movingRecordDetail.timeDroveString;
        //userAvatar;
        MovingRecordDetailScene.SetActive(true);
    }

    void ShowDistance()
    {
        textDistanceRecord.text = _movingRecordDetail.distance.ToString("0.0");
    }

    public void PostRecords()
    {
        Debug.LogWarning("Save Record");
    }

    void OnAddAMovingRecord(string nameProcedure,string message, int Id)
    {
        Debug.Log("OnAddAMovingRecord");
    }

}
