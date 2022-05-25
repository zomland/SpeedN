using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    MovingRecord _movingRecord;

    public void CreateMovingRecord(float _numCoin, string _vehicleName, float _distance
        , string _timeDroveString)
    {
        string timeCreate = System.DateTime.Now.ToString();
        _movingRecord = new MovingRecord(timeCreate, _numCoin, _vehicleName, _distance, _timeDroveString);
    }
    public void DisplayMovingRecord()
    {
        textMessageMovingRecord.text = "Using vehicle: " + _movingRecord.vehicleName;
        currentVehicleRawImg.texture
            = ClientData.Instance.GetSpriteVehicle(_movingRecord.vehicleName).sprite.texture;
        textUserName.text = ClientData.Instance.ClientUser.userName;
        textTime.text = _movingRecord.time;
        textNumCoinRecord.text = _movingRecord.numCoin.ToString("0.0");
        ShowDistance();
        textTimeDrove.text = _movingRecord.timeDroveString;
        //userAvatar;
        MovingRecordDetailScene.SetActive(true);
    }

    void ShowDistance()
    {
        textDistanceRecord.text = _movingRecord.distance.ToString("0.0");
    }

    public void SaveRecord()
    {
        Debug.LogWarning("Save Record");
    }

}
