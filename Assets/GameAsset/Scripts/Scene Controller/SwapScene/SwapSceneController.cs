using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSceneController : MonoBehaviour
{
    [Header("Prefab")]
    public SwapSceneItem itemSend;
    public SwapSceneItem itemGet;

    [Header("List")]
    public GameObject[] list;
    public GameObject[] whereSpawn;

    bool [] isSpawn ={false,false};
    

    public void OnClickChooseCoin(int index)
    {
        // if(isSpawn[index]) return;
        // isSpawn[index] = true;
        // if(index == 0)
        // {      
        //     foreach(var child in ClientData.Instance.ClientCoin.Coins)
        //     {
        //         var item = Instantiate(itemSend,whereSpawn[index].transform.position , Quaternion.identity, list[index].transform);
        //         item.SetProperties(child.nameCoin);
        //     }
        // }
        // else if(index ==1)
        // {
        //     foreach(var child in ClientData.Instance.ClientCoin.Coins)
        //     {
        //         var item = Instantiate(itemGet,whereSpawn[index].transform.position , Quaternion.identity, list[index].transform);
        //         item.SetProperties(child.nameCoin);
        //     }
        // }
    }

    public void ChooseCoin(int index)
    {
            for(int i = 0;i< list[index].transform.childCount;i++)
            {
                list[index].transform.GetChild(i).GetComponent<SwapSceneItem>().choice.gameObject.SetActive(false);
            }    
    }
}
