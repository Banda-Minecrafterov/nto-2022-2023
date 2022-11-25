using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurseAuka : CurseMaster
{
    [SerializeField]
    Image image;


    protected override void ApplyEffect()
    {
        Debug.Log("Effect applyed: " + stacks);
        if (stacks == 0)
        {
            if (isIncreasing)
                image.gameObject.SetActive(true);
            else
                image.gameObject.SetActive(false);
        }

        var color = image.color;
        color.a = (float)stacks / S;
        image.color = color;
    }
}
