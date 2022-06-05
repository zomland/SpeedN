using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockMonitorControler : MonoBehaviour
{
    public Text textValueShow;
    public Text textMaxValue;
    public Text textMinValue;
    public Image FillImg;
    public RectTransform HandClockRect;
    public bool isPercent;
    public bool isUseLimitString;
    public string MinString;
    public string MaxString;
    public string Unit;
    public string Sign;
    public string formatTextValue;
    public bool isOverRangeValue;

    float ValueShowPercent;
    float ValueCurrentPercent = 0;
    float SpeedHandClock = 0.01f;
    float DeltaHandClock = 0.006f;
    float maxValue;
    float minValue;
    float value;


    public void Initialize(float[] _limitValue)
    {
        SetLimitValue(_limitValue);
        value = minValue;
        SetLimitString();
    }

    public void SetLimitValue(float[] _limitValue)
    {
        maxValue = _limitValue[1];
        minValue = _limitValue[0];
    }

    public void SetValue(float _value)
    {
        if (_value >= minValue & _value <= maxValue)
        {
            value = _value;
        }
        else
        {
            if (_value < minValue) value = minValue;
            else value = maxValue;
        }
        ValueShowPercent = (value - minValue) / (maxValue - minValue);

    }

    public void UpdateValuePercent()
    {
        if (Mathf.Abs(ValueShowPercent - ValueCurrentPercent) > DeltaHandClock)
        {
            if (ValueShowPercent > ValueCurrentPercent)
            {
                ValueCurrentPercent += SpeedHandClock;
            }
            else
            {
                ValueCurrentPercent -= SpeedHandClock;
            }
        }
        if (1 - ValueCurrentPercent < DeltaHandClock)
        {
            ValueCurrentPercent = 1;
        }
        if (ValueCurrentPercent < DeltaHandClock)
        {
            ValueCurrentPercent = 0;
        }

    }

    #region UI
    void UpdateFill()
    {
        FillImg.fillAmount = ValueCurrentPercent;
    }
    void UpdateHandClock()
    {
        Vector3 newEulerAngles = new Vector3(0f, 0f, 180f - ValueCurrentPercent * 180f);
        HandClockRect.eulerAngles = newEulerAngles;
    }
    void UpdateTextValueShow()
    {
        string ValueString;
        if (isPercent)
        {
            ValueString = (ValueCurrentPercent * 100f).ToString(formatTextValue);
        }
        else
        {
            ValueString = value.ToString(formatTextValue);
        }

        textValueShow.text = Sign + " " + ValueString + " " + Unit;
    }
    void SetLimitString()
    {
        if (isUseLimitString)
        {
            textMaxValue.text = MaxString;
            textMinValue.text = MinString;
        }
        else
        {
            textMaxValue.text = maxValue.ToString("0");
            textMinValue.text = minValue.ToString("0");
        }
    }
    public void ChangeUnit(string _unit)
    {
        Unit = _unit;
    }
    #endregion

    void Update()
    {
        UpdateValuePercent();
        UpdateFill();
        UpdateHandClock();
        UpdateTextValueShow();
    }

}
