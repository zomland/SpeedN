using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItemSceneController : MonoBehaviour
{
    public GameObject listCarItems;
    public GameObject whereToSpawn;

    [Header("Prefab Car")]
    public MyItemSceneItemVehicle carItem;

    void Start()
    {
        CreateItemCar();
    }

    private void CreateItemCar()
    {
        foreach(var child in ClientData.Instance.clientUser.clientNFT.clientVehicles)
        {
            var item = Instantiate(carItem,whereToSpawn.transform.position, Quaternion.identity,listCarItems.transform);
            item.SetProperties(child);
        }
    }
}
