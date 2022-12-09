using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class Bar : MonoBehaviour
{
    protected Slider slider;


    protected void Awake()
    {
        slider = GetComponent<Slider>();
    }


    public virtual void SetMax(float health)
    {
        slider.maxValue = health;
        slider.value    = health;
    }

    public virtual void SetValue(float health)
    {
        slider.value = health;
    }
}
