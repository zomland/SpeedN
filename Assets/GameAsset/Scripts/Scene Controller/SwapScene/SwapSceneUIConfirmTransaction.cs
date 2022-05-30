using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FirebaseHandler;
using Global;

public class SwapSceneUIConfirmTransaction : MonoBehaviour
{
    public Image [] imageCoin;
    public TextMeshProUGUI [] amountCoinText;
    public TextMeshProUGUI [] typeCoinText;

    SwapSceneData swapSceneData;

    void Start()
    {
        swapSceneData = FindObjectOfType<SwapSceneData>();
    }

    public void DisplayUI(List<ClientCoin> swapCoin)
    {
        for(int i =0 ; i< 2 ;i++)
        {
            imageCoin[i].sprite = ClientData.Instance.GetSpriteIcon(swapCoin[i].nameCoin).sprite;
            amountCoinText[i].text  = swapCoin[i].amount.ToString();
            typeCoinText[i].text = swapCoin[i].nameCoin;
        }
    }

    public void  OnClickConfirmButton()
    {
        ClientData.Instance.ClientUser.SwapCoin(swapSceneData.swapCoin[0].nameCoin , swapSceneData.swapCoin[1].nameCoin ,swapSceneData.swapCoin[0].amount,swapSceneData.swapCoin[1].amount  );
        List<ClientCoin> newClientCoin =  ClientData.Instance.ClientUser.clientCoins;

        FirebaseApi.Instance.PostUserValue("clientCoins",newClientCoin);
        
    }
}
