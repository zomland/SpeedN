using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmountCoin : MonoBehaviour
{
    public TextMeshProUGUI amountCoinText;

    void Start()
    {
        amountCoinText.text =  ClientData.Instance.ClientUser.numCoin.ToString();        
    }
}
