using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OpenAlpha : OpenWhenInteracted
{
    [SerializeField]
    SpriteRenderer open;


    protected override void Interact()
    {
        base.Interact();
        StartCoroutine(Open());
    }

    IEnumerator Open()
    {
        yield return null;
        while (true)
        {
            Color color = open.color;
            color.a = 1.0f - animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            open.color = color;

            yield return null;
        }
    }


    public new void StopInteracting()
    {
        base.StopInteracting();
        open.gameObject.SetActive(false);
    }


    public override void Load(BinaryReader data, int version)
    {
        open.gameObject.SetActive(LoadProtected(data, version));
    }
}
