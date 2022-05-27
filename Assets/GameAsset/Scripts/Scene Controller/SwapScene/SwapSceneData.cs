using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSceneData : MonoBehaviour
{
    public List<ClientCoin> swapCoin = new List<ClientCoin>();
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
            var tmp  = ClientData.Instance.ClientUser.clientCoins[i];
            swapCoin.Add(tmp);
        }
        swapUIController.DisplaySwapScene();

    }
}
