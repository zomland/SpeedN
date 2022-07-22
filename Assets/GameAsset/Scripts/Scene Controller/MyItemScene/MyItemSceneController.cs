using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GUIHandler;

public class MyItemSceneController : MonoBehaviour
{
    public GameObject listCarItems;
    public GameObject whereToSpawn;

    public GameObject listShoesItem;
    public GameObject listBicycleItem;

    [Header("Prefab Car")]
    public MyItemSceneItemVehicle carItem;

    void Start()
    {
        CreateItemCar();
    }

    private void CreateItemCar()
    {
        foreach (var child in ClientData.Instance.ClientUser.clientVehicle.Vehicles)
        {
            if (child.ModelStats().NftType == NFTType.Car)
            {
                var item = Instantiate(carItem, whereToSpawn.transform.position, Quaternion.identity, listCarItems.transform);
                item.SetProperties(child);
            }
            else if (child.ModelStats().NftType == NFTType.Bicycle)
            {
                var item = Instantiate(carItem, Vector3.zero, Quaternion.identity, listBicycleItem.transform);
                item.SetProperties(child);
            }
            else if (child.ModelStats().NftType == NFTType.Shoes)
            {
                var item = Instantiate(carItem, Vector3.zero, Quaternion.identity, listShoesItem.transform);
                item.SetProperties(child);
            }
        }
        Invoke("ResizeContents", 0.1f);
    }
    void ResizeContents()
    {
        GUIManager.ResizeScrollRectContent(listCarItems);
        GUIManager.ResizeScrollRectContent(listShoesItem);
        GUIManager.ResizeScrollRectContent(listBicycleItem);
        //GUIManager.ResizeScrollRectContent(listSportStore);
    }
}
