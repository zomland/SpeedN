using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRecordOnAccountControler : MonoBehaviour
{
    public bool isOnMovingRecordState = false;
    public GameObject PoolButtons;
    public GameObject ContentButton;
    public int MaxRecordCanShow = 5;
    // Start is called before the first frame update
    void Start()
    {
        if (isOnMovingRecordState)
        {
            InitialLoadButtons();
        }
    }

    void InitialLoadButtons()
    {
        if (ClientData.Instance.clientMovingRecord.movingRecordDetails[0].distance != 0)
        {
            int maxIndex;
            int numRecord = ClientData.Instance.clientMovingRecord.AmountRecord();
            if (MaxRecordCanShow > numRecord) maxIndex = numRecord;
            else maxIndex = MaxRecordCanShow;
            for (int index = 0; index < maxIndex; index++)
            {
                GameObject button = ButtonGetFromPool(false);
                button.GetComponent<ButtonToRecordDetailControler>().indexRecordDetail = index;
                button.GetComponent<ButtonToRecordDetailControler>().DisplayButtonInfo();
            }
        }
    }

    GameObject ButtonGetFromPool(bool isOnTop)
    {
        GameObject button = PoolButtons.transform.GetChild(0).gameObject;
        button.transform.SetParent(ContentButton.transform, false);
        if (isOnTop) button.transform.SetAsFirstSibling();
        else button.transform.SetAsLastSibling();
        return button;
    }

    GameObject ButtonStore(bool isOnTop)
    {
        if (ContentButton.transform.childCount > 0)
        {
            GameObject button;
            if (isOnTop) button = ContentButton.transform.GetChild(0).gameObject;
            else button = ContentButton.transform.GetChild(ContentButton.transform.childCount - 1).gameObject;
            button.transform.SetParent(PoolButtons.transform, false);
            button.transform.SetAsLastSibling();
            return button;
        }
        return null;

    }



    // Update is called once per frame
    void Update()
    {

    }
}
