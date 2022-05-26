using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSceneData : MonoBehaviour
{
    public List<ClientCoin> swapCoin;

    public void InitSwap()
    {
        swapCoin = new List<ClientCoin>();
        for(int i = 0 ;i< 2 ;i++)
        {
            swapCoin[i] = ClientData.Instance.ClientUser.clientCoins[i];
        }
    }
}
