using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class random : MonoBehaviour
{
    public TextAsset textAssetData;
    public Text text;
    public Text idtext;
    public Text nametext;
    public Text pricetext;
    [Serializable]
    public class Item
    {
        public int id;
        public string name;
        public int price;
    }

    [Serializable]
    public class ItemList
    {
        public Item[] items;
    }

    public ItemList itemListData = new ItemList();
   
    // Start is called before the first frame update
    void Start()
    {
        ReadCSV();    
    }
     void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {          
            int a = UnityEngine.Random.Range(0, itemListData.items.Length);
            text.text = a.ToString();
            idtext.text = (itemListData.items[a].id).ToString();
            nametext.text = (itemListData.items[a].name).ToString();
            pricetext.text = (itemListData.items[a].price).ToString();
        }

    }
    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int Size = data.Length / 3 - 1;

        itemListData.items = new Item[Size];

        for (int i = 0; i < Size; i++)
        {
            itemListData.items[i] = new Item();
            itemListData.items[i].id = int.Parse(data[3 * (i + 1)]);
            itemListData.items[i].name = data[ 3 * (i + 1 ) + 1 ];
            itemListData.items[i].price = int.Parse(data[ 3 * (i + 1) + 2]);
        }
    }
    
}
