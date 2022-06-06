using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingRecordOnAccountControler : MonoBehaviour
{
    public bool isOnMovingRecordState = false;
    const int MaxRecordCanShow = 5;
    public ButtonToRecordDetailControler[] ButtonsCtrl;
    public Button buttonBack;
    public Button buttonForward;
    public Text textPage;
    int lastPage;
    public enum LoadButtonBehaviour
    {
        GoBack = -MaxRecordCanShow, GoForward = MaxRecordCanShow, Initialize = 0
    }


    // Start is called before the first frame update
    void Start()
    {
        lastPage = ClientData.Instance.clientMovingRecord.AmountRecord() / MaxRecordCanShow;
        if (ClientData.Instance.clientMovingRecord.AmountRecord() % MaxRecordCanShow > 0)
            lastPage++;
        LoadButtonsRecordDetails((int)LoadButtonBehaviour.Initialize);
    }

    void ProcessPagination()
    {
        // buttons
        if (ButtonsCtrl[0].indexRecordDetail <= 0) buttonBack.interactable = false;
        else buttonBack.interactable = true;

        if (ButtonsCtrl[ButtonsCtrl.Length - 1].indexRecordDetail
            >= ClientData.Instance.clientMovingRecord.AmountRecord() - 1
            | ButtonsCtrl[ButtonsCtrl.Length - 1].indexRecordDetail == -1)
            buttonForward.interactable = false;
        else buttonForward.interactable = true;

        //textPage
        int currentPage = ButtonsCtrl[0].indexRecordDetail / MaxRecordCanShow + 1;
        textPage.text = currentPage.ToString() + "/" + lastPage.ToString();
    }

    public void LoadButtonsRecordDetails(int otpLoadButtonBehaviour)
    {
        int firstIndex = 0;
        switch (otpLoadButtonBehaviour)
        {
            case (int)LoadButtonBehaviour.Initialize:
                firstIndex = 0;
                break;
            case (int)LoadButtonBehaviour.GoBack:
                if (ButtonsCtrl[0].indexRecordDetail != 0) firstIndex = ButtonsCtrl[0].indexRecordDetail - MaxRecordCanShow;
                break;
            case (int)LoadButtonBehaviour.GoForward:
                if (ButtonsCtrl[0].indexRecordDetail + MaxRecordCanShow <= ClientData.Instance.clientMovingRecord
                    .AmountRecord()) firstIndex = ButtonsCtrl[0].indexRecordDetail + MaxRecordCanShow;
                break;
            default:
                firstIndex = 0;
                Debug.LogError("LoadButtonsRecordDetails: Exception");
                break;
        }
        for (int index = 0; index < MaxRecordCanShow; index++)
        {
            if (firstIndex + index < ClientData.Instance.clientMovingRecord.AmountRecord())
            {
                ButtonsCtrl[index].indexRecordDetail = firstIndex + index;
                ButtonsCtrl[index].DisplayButtonInfo();
                ButtonsCtrl[index].gameObject.SetActive(true);
            }
            else
            {
                ButtonsCtrl[index].indexRecordDetail = -1;
                ButtonsCtrl[index].gameObject.SetActive(false);
            }
        }
        ProcessPagination();
    }

}
