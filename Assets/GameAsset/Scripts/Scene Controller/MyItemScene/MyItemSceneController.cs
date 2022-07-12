using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyItemSceneController : MonoBehaviour
{
    public GameObject listCarItems;
    public GameObject whereToSpawn;

    [SerializeField] private Transform listShoesItem;
    [SerializeField] private Transform listBicycleItem;

    [Header("Prefab Car")]
    public MyItemSceneItemVehicle carItem;

    void Start()
    {
        CreateItemCar();
    }

    private void CreateItemCar()
    {
        foreach(var child in ClientData.Instance.ClientVehicle.Vehicles)
        {
            if (child.BaseStats.NftType == NFTType.Car)
            {
                var item = Instantiate(carItem,whereToSpawn.transform.position, Quaternion.identity,listCarItems.transform);
                item.SetProperties(child);
            }
            else if (child.BaseStats.NftType == NFTType.Bicycle)
            {
                var item = Instantiate(carItem,Vector3.zero, Quaternion.identity,listBicycleItem.transform);
                item.SetProperties(child);
            }
            else if (child.BaseStats.NftType == NFTType.Shoes)
            {
                var item = Instantiate(carItem,Vector3.zero, Quaternion.identity,listShoesItem.transform);
                item.SetProperties(child);
            }
        }
    }
}
