using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OpenWhenInteracted : Interactable, ISaveLoadData
{
    [SerializeField]
    SpriteRenderer open;

    [SerializeField]
    float speed;

    [SerializeField]
    int id;


    void Awake()
    {
        SaveLoadManager.AddObject(SaveLoadManager.SaveObjectId.openWhenInteracted0 + id, this);
    }


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
        foreach(var i in GetComponentsInChildren<Collider2D>())
        {
            i.enabled = false;
        }

        enabled = false;
    }


    public void Load(ref BinaryReader data, int version)
    {
        if (!data.ReadBoolean())
        {
            Interact();
        }
    }

    public void Save(ref BinaryWriter data)
    {
        data.Write(open.gameObject.activeSelf);
    }
}
