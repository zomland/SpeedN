using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MyWalletItem : MonoBehaviour
{ 
    public Image icon;
    public TextMeshProUGUI nameCoinText;
    public TextMeshProUGUI amountCoinText;
    
    MyWalletUIController myWalletUIController;
    Coin coin;

    void Start(){
        myWalletUIController = FindObjectOfType<MyWalletUIController>();
    }

    public void SetProperties(Coin coin)
    {
        this.coin = coin;
        this.nameCoinText.text = coin.nameCoin;
        this.icon.sprite = ClientData.Instance.GetSpriteIcon(coin.nameCoin).sprite;
        this.amountCoinText.text = coin.amount.ToString();
    }

    public void OnClickItemCoin(){
        myWalletUIController.DisplayCoin(coin);
    }
}
