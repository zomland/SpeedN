using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderControler : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener((v) =>
        {
            text.text = v.ToString("00" + " VND");
        });
    }
}
