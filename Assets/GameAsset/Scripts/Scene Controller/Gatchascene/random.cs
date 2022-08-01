using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Text;
using System.IO;

public class random : MonoBehaviour
{
    public TextAsset textAssetData;
    public Text text;
    public Text idtext;
    public Text nametext;
    public Text pricetext;


    public Image ima;
    public Sprite[] sp;

    [Serializable]
    public class Item
    {
        public int id;
        public string name;
        public int price;
        public string pic;
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

            text.text = (a + 1).ToString();
            idtext.text = (itemListData.items[a].id).ToString();
            nametext.text = (itemListData.items[a].name).ToString();
            pricetext.text = (itemListData.items[a].price).ToString();
            int num = int.Parse(itemListData.items[a].pic);

            if (String.Compare(num.ToString(), sp[a].name, true) == 0)
            {
                ima.sprite = sp[a];
            }

            remove();
        }

    }
    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int Size = data.Length / 4 - 1;

        itemListData.items = new Item[Size];

        for (int i = 0; i < Size; i++)
        {
            itemListData.items[i] = new Item();
            itemListData.items[i].id = int.Parse(data[4 * (i + 1)]);
            itemListData.items[i].name = data[4 * (i + 1) + 1];
            itemListData.items[i].price = int.Parse(data[4 * (i + 1) + 2]);
            itemListData.items[i].pic = data[4 * (i + 1) + 3];
        }
    }
    void remove()
    {
        if(itemListData.items.Length > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            
            tw = new StreamWriter(filename, true);
            tw.WriteLine(itemListData.items[1].id);
        }
        
    }
}

