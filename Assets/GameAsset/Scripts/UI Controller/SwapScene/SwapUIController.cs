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
    public TMP_InputField[] input;

    SwapSceneData swapSceneData;
    float[] amount = {0,0} ;
 

    void Start()
    {
         swapSceneData = GetComponent<SwapSceneData>();
    }

    void Update()
    {
        float.TryParse(input[0].text, out amount[0]);
        float.TryParse(input[1].text, out amount[1]);
        Debug.Log(amount[0]);
        Debug.Log(amount[1]);
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

    public void OnClickMaxButton(int index)
    {
        swapSceneData.swapCoin[index].amount =  ClientData.Instance.ClientUser.clientCoins[index].amount;
        input[index].text =  swapSceneData.swapCoin[index].amount.ToString();
    }
}
