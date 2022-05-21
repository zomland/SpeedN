using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockMonitorControler : MonoBehaviour
{
    public Text textShowValue;
    public SliderControler sliderControler;
    public Text textMaxValue;
    public Text textMinValue;

    public string MaxValue;
    public string MinValue;
    public string Unit;
    public string Sign;
    public string formatTextValue;
    public float Coefficient;// he so
    float ValueShow;
    // Start is called before the first frame update
    void Start()
    {
        textMaxValue.text = MaxValue;
        textMinValue.text = MinValue;
    }

    public void SetMaxValue(float _MaxValue)
    {
        MaxValue = _MaxValue.ToString("0");
        textMaxValue.text = MaxValue;
    }

    public void SetValueShow(float _valueShow)
    {
        ValueShow = _valueShow;
    }

    void UpdateTextShowValue()
    {
        textShowValue.text = Sign + (ValueShow * Coefficient).ToString(formatTextValue) + Unit;
    }
    void UpdateSlider()
    {
        sliderControler.SetValueShow(ValueShow);
    }

    void UpdateClockMonitor()
    {
        UpdateSlider();
        UpdateTextShowValue();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateClockMonitor();
    }

}
