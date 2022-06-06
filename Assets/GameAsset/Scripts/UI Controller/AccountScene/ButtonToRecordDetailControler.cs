using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonToRecordDetailControler : MonoBehaviour
{
    public int indexRecordDetail;
    public Text textTime;
    public Text textDistance;
    public Text textTimeDrove;
    public MovingRecordDetailControler recordDetailControler;
    public AccountUIControler accountUIControler;

    public void DisplayButtonInfo()
    {
        if (indexRecordDetail >= 0)
        {
            MovingRecordDetail _detail = ClientData.Instance.clientMovingRecord
                .movingRecordDetails[indexRecordDetail];
            textTime.text = _detail.time;
            textDistance.text = _detail.distance.ToString();
            textTimeDrove.text = _detail.timeDroveString;
        }

    }

    public void OnButtonDetail()
    {
        if (indexRecordDetail >= 0)
        {
            recordDetailControler.LoadDataMovingRecord(indexRecordDetail);
            recordDetailControler.DisplayMovingRecord();
            accountUIControler.ActiveCanvas("MovingRecordDetail");
        }
    }

}
