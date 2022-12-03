using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    public Slider slider;
    public Image fill;
    public void MaxHealthPoint(int health)
    {
        slider.maxValue = health;
        slider.value = health;

    }
    public void HealthPoint(int health)
    {
        slider.value = health;
    }
}
