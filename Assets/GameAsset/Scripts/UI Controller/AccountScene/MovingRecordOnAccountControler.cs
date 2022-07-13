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
        Debug.LogError(ClientData.Instance.ClientUser.clientMovingRecord.AmountRecord());
        int _amountRecord = ClientData.Instance.ClientUser.clientMovingRecord.AmountRecord();
        lastPage = _amountRecord / MaxRecordCanShow;
        if (_amountRecord % MaxRecordCanShow > 0)
            lastPage++;
        LoadButtonsRecordDetails((int)LoadButtonBehaviour.Initialize);
    }

    void ProcessPagination()
    {
        int _amountRecord = ClientData.Instance.ClientUser.clientMovingRecord.AmountRecord();

        // buttons
        if (ButtonsCtrl[0].indexRecordDetail >= _amountRecord - 1) buttonBack.interactable = false;
        else buttonBack.interactable = true;

        if (ButtonsCtrl[ButtonsCtrl.Length - 1].indexRecordDetail <= 1
            | ButtonsCtrl[ButtonsCtrl.Length - 1].indexRecordDetail == -1)
            buttonForward.interactable = false;
        else buttonForward.interactable = true;

        //textPage
        int currentPage = (_amountRecord - ButtonsCtrl[0].indexRecordDetail)
            / MaxRecordCanShow + 1;
        textPage.text = currentPage.ToString() + "/" + lastPage.ToString();
    }

    public void LoadButtonsRecordDetails(int otpLoadButtonBehaviour)
    {
        int _amountRecord = ClientData.Instance.ClientUser.clientMovingRecord.AmountRecord();
        int firstIndex = -777;
        switch (otpLoadButtonBehaviour)
        {
            case (int)LoadButtonBehaviour.Initialize:
                firstIndex = _amountRecord - 1;
                break;
            case (int)LoadButtonBehaviour.GoBack:
                if (ButtonsCtrl[0].indexRecordDetail != _amountRecord - 1)
                    firstIndex = ButtonsCtrl[0].indexRecordDetail + MaxRecordCanShow;
                break;
            case (int)LoadButtonBehaviour.GoForward:
                if (ButtonsCtrl[0].indexRecordDetail - MaxRecordCanShow >= 1)
                    firstIndex = ButtonsCtrl[0].indexRecordDetail - MaxRecordCanShow;
                break;
            default:
                Debug.LogError("LoadButtonsRecordDetails: Exception");
                break;
        }
        if (firstIndex != -777)
            for (int index = 0; index < MaxRecordCanShow; index++)
            {
                if (firstIndex - index >= 0)
                {
                    ButtonsCtrl[index].indexRecordDetail = firstIndex - index;
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
