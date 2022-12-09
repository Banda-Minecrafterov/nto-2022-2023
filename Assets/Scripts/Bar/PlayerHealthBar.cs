using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : Bar
{
    [SerializeField]
    Gradient gradient;

    Image fill;


    new void Awake()
    {
        fill = GetComponentInChildren<Image>();

        base.Awake();
    }


    public override void SetMax(float health)
    {
        base.SetMax(health);

        fill.color = gradient.Evaluate(1f);
    }
    public override void SetValue(float health)
    {
        base.SetValue(health);

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
