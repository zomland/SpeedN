using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MyWalletController : MonoBehaviour
{
    public GameObject listCoin;
    public GameObject myWalletItemPrefab;
    public Transform whereToSpawn;

    int totalTypeCoin;
     List<Coin> sortedList = new List<Coin>();
    
    void Start(){
        totalTypeCoin= ClientData.Instance.speedNDefault.spriteIcons.Count;
        SortAmountCoin();
        CreateListCoin();
    }

    private void SortAmountCoin(){
        sortedList = ClientData.Instance.ClientCoin.Coins.OrderByDescending(coin=>coin.amount).ToList();
    }

    private void CreateListCoin(){
        foreach(var child in sortedList)
        {
             GameObject item = Instantiate(myWalletItemPrefab,whereToSpawn.position,Quaternion.identity,listCoin.transform);
             
            item.GetComponent<MyWalletItem>().SetProperties(child);
        }
    }
}
