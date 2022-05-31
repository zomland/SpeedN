using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SwapUIController : MonoBehaviour
{
    public Image[] icon;
    public TextMeshProUGUI[] nameCoin;
    public TextMeshProUGUI[] amountText;
    public TMP_InputField inputSend;
    public TextMeshProUGUI amountGet;

    [Header("PopUp")]
    public GameObject warningPopup;
    public GameObject confirmTransactionPopup;

    SwapSceneData swapSceneData;

    void Start()
    {
         swapSceneData = GetComponent<SwapSceneData>();
    }

    void Update()
    {
        float.TryParse(inputSend.text, out swapSceneData.swapCoin[0].amount);
        swapSceneData.ConvertCoinToCoin();
        amountGet.text =  swapSceneData.swapCoin[1].amount.ToString();
    }


    public void DisplaySwapScene()
    {  
        for(int i=0;i<2;i++)
        {
            icon[i].sprite = ClientData.Instance.GetSpriteIcon(swapSceneData.swapCoin[i].nameCoin).sprite;
            nameCoin[i].text =  swapSceneData.swapCoin[i].nameCoin;
            amountText[i].text = ClientData.Instance.ClientUser.GetAmountCoin(swapSceneData.swapCoin[i].nameCoin).ToString();
        }
    }


    // On click Button
    public void OnClickMaxButton()
    {
        swapSceneData.swapCoin[0].amount =  ClientData.Instance.ClientUser.GetAmountCoin(swapSceneData.swapCoin[0].nameCoin);
        inputSend.text =  swapSceneData.swapCoin[0].amount.ToString();
    }

    public void OnClickConfirmTransaction()
    {
        if(swapSceneData.swapCoin[0].amount > ClientData.Instance.ClientUser.GetAmountCoin(swapSceneData.swapCoin[0].nameCoin) )
        {
            warningPopup.gameObject.SetActive(true);
        }
        else
        {
            confirmTransactionPopup.gameObject.SetActive(true);
            confirmTransactionPopup.GetComponent<SwapSceneUIConfirmTransaction>().DisplayUI(swapSceneData.swapCoin);
        }
    }
}
