using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonToRecordDetailControler : MonoBehaviour
{
    public int indexRecordDetail;
    public Text textTime;
    public Text textDistance;
    public Text textTimeDrove;
    public MovingRecordControler recordControler;
    public AccountUIControler accountUIControler;

    public void DisplayButtonInfo()
    {
        if (indexRecordDetail >= 0)
        {
            MovingRecord _detail = ClientData.Instance.ClientUser.clientMovingRecord
                .movingRecords.ElementAt(indexRecordDetail).Value;
            textTime.text = _detail.Time;
            textDistance.text = _detail.Distance.ToString() + " km";
            textTimeDrove.text = _detail.TimeDroveString;
        }

    }

    public void OnButtonDetail()
    {
        if (indexRecordDetail >= 0)
        {
            recordControler.LoadDataMovingRecord(indexRecordDetail);
            recordControler.DisplayMovingRecord();
            accountUIControler.ActiveCanvas("MovingRecordDetail");
        }
    }

}
