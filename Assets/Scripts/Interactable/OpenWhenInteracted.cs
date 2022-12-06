using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OpenWhenInteracted : InteractableDisableIfInteract
{
    [SerializeField]
    SpriteRenderer open;

    [SerializeField]
    float speed;


    protected override void Interact()
    {
        StopButtonCheck();

        StartCoroutine(Open());
    }


    IEnumerator Open()
    {
        while (true)
        {
            Color color = open.color;
            color.a -= Time.deltaTime * speed;
            open.color = color;

            if (color.a <= 0)
                break;
            yield return null;
        }
        open.gameObject.SetActive(false);

        GetComponent<Collider2D>().enabled = false;
        foreach (var i in GetComponentsInChildren<Collider2D>())
        {
            i.enabled = false;
        }

        enabled = false;
    }


    protected override SaveLoadManager.SaveObjectId GetSaveObjectId()
    {
        return SaveLoadManager.SaveObjectId.openWhenInteracted0;
    }
}
