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

    public Text nametext;
    public Text pricetext;
    public Image ima;
    public Sprite[] sp;
    public Animator animator;
    public Animator itemanimator;
    public AnimationClip clip;
    public GameObject chestmain;
    public GameObject caritem;
    [SerializeField] private Button buttonop;
    [SerializeField] private Button buttoncl;
    [Serializable]
    public class Item
    {
        public string id;
        public string name;
        public int price;
        public string pic;

        public bool Equals(Item other)
        {
            if (other == null) return false;
            return (this.id.Equals(other.id));
        }
    }

    public List<Item> items = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        ReadCSV();
    }

    //read data
    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        int Size = data.Length / 4 - 1;

        for (int i = 0; i < Size; i++)
        {

            items.Add(new Item()
            {
                id = data[4 * (i + 1)]
                ,
                name = data[4 * (i + 1) + 1]
                ,
                price = int.Parse(data[4 * (i + 1) + 2])
                ,
                pic = data[4 * (i + 1) + 3]
            });
        }

    }

    public void Open()
    {
        StartCoroutine(wait());

    }
    IEnumerator wait()
    {
        buttoncl.enabled = false;
        animator.SetBool("opencofirm", true);
        yield return new WaitForSeconds(3f);
        animator.SetBool("opencofirm", false);

        caritem.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        itemanimator.SetBool("ok", true);

        int a = UnityEngine.Random.Range(0, items.Count);

        //show
        nametext.text = (items[a].name).ToString();
        pricetext.text = (items[a].price).ToString();
        ima.sprite = sp[int.Parse(items[a].pic)];
        //remove
        items.RemoveAt(a);

        yield return new WaitForSeconds(2.5f);
        animator.SetBool("back", true);
        chestmain.SetActive(false);
        buttoncl.enabled = true;
    }
    public void close()
    {
        chestmain.SetActive(true);
        caritem.SetActive(false);
        itemanimator.SetBool("ok", false);
    }
}

