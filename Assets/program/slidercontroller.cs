using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class slidercontroller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderText = null;
    
    [SerializeField] private float maxSliderAmount = 5.0f;

    public void SliderChange(float value)
    {
        float localValue = value * maxSliderAmount;
        sliderText.text = localValue.ToString("0.0");
    }


}
