using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControler : MonoBehaviour
{

    public Image FillImg;
    public RectTransform HandClockRect;

    [Range(0f, 1f)]
    public float valueShow;

    public float currentValue = 0;
    public float deltaValue = 0.02f;
    public float clockHandSpeed = 0.01f;

    void UpdateFill()
    {
        FillImg.fillAmount = currentValue;
    }

    void UpdateHandClock()
    {
        Vector3 newEulerAngles = new Vector3(0f, 0f, 180f - currentValue * 180f);
        HandClockRect.eulerAngles = newEulerAngles;
    }

    public void UpdateValue()
    {
        if (Mathf.Abs(valueShow - currentValue) > deltaValue)
        {
            if (valueShow > currentValue)
            {
                currentValue += clockHandSpeed;
            }
            else
            {
                currentValue -= clockHandSpeed;
            }
        }
    }

    public void SetValueShow(float newValue)
    {
        valueShow = newValue;
    }

    private void FixedUpdate()
    {
        UpdateValue();
        UpdateFill();
        UpdateHandClock();
    }


}
