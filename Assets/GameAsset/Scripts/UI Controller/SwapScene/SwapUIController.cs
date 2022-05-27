using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SwapUIController : MonoBehaviour
{
    public Image[] icon;
    public TextMeshProUGUI[] nameCoin;
    public TextMeshProUGUI[] amount;
    public TMP_InputField[] input;


    public void DisplaySwapScene()
    {  
        var swapSceneData = GetComponent<SwapSceneData>();
        for(int i=0;i<2;i++)
        {
            icon[i].sprite = ClientData.Instance.GetSpriteIcon(swapSceneData.swapCoin[i].nameCoin).sprite;
            nameCoin[i].text =  swapSceneData.swapCoin[i].nameCoin;
            amount[i].text = swapSceneData.swapCoin[i].amount.ToString();
        }
    }
}
