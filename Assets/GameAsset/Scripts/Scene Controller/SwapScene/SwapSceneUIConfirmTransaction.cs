using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FirebaseHandler;
using Global;
using Cysharp.Threading.Tasks;



public class SwapSceneUIConfirmTransaction : MonoBehaviour
{
    public Image[] imageCoin;
    public TextMeshProUGUI[] amountCoinText;
    public TextMeshProUGUI[] typeCoinText;

    SwapSceneData swapSceneData;

    void Start()
    {
        swapSceneData = FindObjectOfType<SwapSceneData>();
    }

    public void DisplayUI(List<Coin> swapCoin)
    {
        for (int i = 0; i < 2; i++)
        {
            imageCoin[i].sprite = ClientData.Instance.GetSpriteIcon(swapCoin[i].nameCoin).sprite;
            amountCoinText[i].text = swapCoin[i].amount.ToString();
            typeCoinText[i].text = swapCoin[i].nameCoin;
        }
    }

    public void OnClickConfirmButton()
    {
        ClientData.Instance.ClientCoin.SwapCoin(swapSceneData.swapCoin[0].nameCoin, swapSceneData.swapCoin[1].nameCoin, swapSceneData.swapCoin[0].amount, swapSceneData.swapCoin[1].amount);
        List<Coin> Coins = ClientData.Instance.ClientCoin.Coins;
        DatabaseHandler.SaveClientCoin(SwapCoinCallback);
        //FirebaseApi.Instance.PostUserValue("clientCoins",newClientCoin,Success);

        swapSceneData.ResetSwapCoin();
        FindObjectOfType<SwapUIController>().DisplaySwapScene();

    }

    void SwapCoinCallback(string message)
    {
        Debug.Log("SwapCoinCallback: " + message);
    }

}
