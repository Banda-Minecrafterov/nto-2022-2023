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


    public override void MaxHealthPoint(float health)
    {
        base.MaxHealthPoint(health);

        fill.color = gradient.Evaluate(1f);
    }
    public override void HealthPoint(float health)
    {
        base.HealthPoint(health);

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
