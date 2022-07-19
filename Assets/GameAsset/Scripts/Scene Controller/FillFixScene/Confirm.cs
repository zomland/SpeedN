using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Confirm : MonoBehaviour
{
    [SerializeField] private Button button;
    public Text confirmText;
    public Text textMain;
    void Start()
    {
        button.onClick.AddListener(() => {
            textMain.text = confirmText.text;
        });
    }

   
}
