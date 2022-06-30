using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSceneData : MonoBehaviour
{
    public List<Coin> swapCoin = new List<Coin>();
    SwapUIController swapUIController;

    void Start()
    {   
        swapUIController = GetComponent<SwapUIController>();
        InitSwap();
    }

    public void InitSwap()
    {
        for(int i = 0 ;i< 2 ;i++)
        {
            Coin tmp =  new Coin(ClientData.Instance.ClientCoin.Coins[i].nameCoin,0);
            swapCoin.Add(tmp);
        }
        swapUIController.DisplaySwapScene();
    }

    public void ChangeSwapCoin(int index, string nameCoin)
    {
        swapCoin[index].nameCoin = nameCoin;
    }

    public void UpdateAmountSwapCoin(int index , int amount)
    {
        swapCoin[index].amount = amount;
    }

    public void ConvertCoinToCoin()
    {
        swapCoin[1].amount = swapCoin[0].amount /2;
    }

    public void ResetSwapCoin()
    {
        for(int i = 0;i< 2; i++)
        {
            swapCoin[i].amount = 0;
        }
    }
}
