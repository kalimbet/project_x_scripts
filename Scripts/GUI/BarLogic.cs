﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarLogic : MonoBehaviour
{
    public Slider slider;

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }
    
    public void SetMinValue(float value)
    {
        slider.minValue = value;
        slider.value = value;
    }

    public void SetValue(float value)
    {
        slider.value = value;
    }
}
