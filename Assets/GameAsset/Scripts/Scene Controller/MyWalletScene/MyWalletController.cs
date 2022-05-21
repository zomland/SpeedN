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
     List<ClientCoin> sortedList = new List<ClientCoin>();
    
    void Start(){
        totalTypeCoin= ClientData.Instance.speedNDefault.spriteIcons.Count;
        SortAmountCoin();
        CreateListCoin();
    }

    private void SortAmountCoin(){
        sortedList = ClientData.Instance.clientUser.clientCoins.OrderByDescending(coin=>coin.amount).ToList();
    }

    private void CreateListCoin(){
        foreach(var child in sortedList)
        {
             GameObject item = Instantiate(myWalletItemPrefab,whereToSpawn.position,Quaternion.identity,listCoin.transform);

             SpriteIcon spriteItem = ClientData.Instance.GetSpriteIcon(child.nameCoin);
             
            item.GetComponent<MyWalletItem>().SetProperties(spriteItem.sprite,spriteItem.name,
                ClientData.Instance.clientUser.GetAmountCoin(spriteItem.name));
        }
    }
}
