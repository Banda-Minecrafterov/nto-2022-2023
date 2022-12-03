using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    protected Slider slider;


    protected void Awake()
    {
        slider = GetComponent<Slider>();
    }


    public virtual void MaxHealthPoint(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public virtual void HealthPoint(int health)
    {
        slider.value = health;
    }
}
