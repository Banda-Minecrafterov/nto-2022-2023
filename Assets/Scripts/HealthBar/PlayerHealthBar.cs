using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : HealthBar
{
    [SerializeField]
    Gradient gradient;

    Image fill;


    new void Awake()
    {
        fill = GetComponentInChildren<Image>();

        base.Awake();
    }


    public override void MaxHealthPoint(int health)
    {
        base.MaxHealthPoint(health);

        fill.color = gradient.Evaluate(1f);
    }
    public override void HealthPoint(int health)
    {
        base.HealthPoint(health);

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
