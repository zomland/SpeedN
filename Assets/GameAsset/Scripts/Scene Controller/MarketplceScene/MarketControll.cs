using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MarketControll : MonoBehaviour
{
    public Button button;
    public int BNB;
    public int total;
    public GameObject succ;
    public GameObject failed;
    public GameObject thisscn;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(check);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void check()
    {
        if (BNB < total)
        {
            failed.SetActive(true);
            thisscn.SetActive(false);
           
        }
        else
        {
            succ.SetActive(true);
            thisscn.SetActive(false);
        }
    }
}
