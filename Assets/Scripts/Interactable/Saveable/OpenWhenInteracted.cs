using System.Collections;
using UnityEngine;

public class OpenWhenInteracted : SaveableInteractable
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

            yield return null;

            if (color.a <= 0)
                break;
        }
        open.gameObject.SetActive(false);

        GetComponent<Collider2D>().enabled = false;
        foreach (var i in GetComponentsInChildren<Collider2D>())
        {
            i.enabled = false;
        }

        enabled = false;
    }
}
